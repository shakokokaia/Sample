using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour, IDetectable
{
    [Header("Character variables")]
    [SerializeField] Transform _moveLoc;
    [SerializeField] float _rotationSpeed;
    [SerializeField] float _interactionDistance;
    [SerializeField] LayerMask _interactionLayerMask;

    [Header("Gun variables")]
    [SerializeField] string _currentGunID;
    [SerializeField] List<Gun> _guns = new List<Gun>();
    [SerializeField] LayerMask _shootingLayer;

    [Header("Effects")]
    [SerializeField] GameObject _bloodHit;

    [Header("Objects")]
    [SerializeField] GameObject _bodyBag;
    [SerializeField] bool _hasBody;

    private Gun _currentGun;
    private AudioSource _audioSource;
    private NavMeshAgent _navAgent;
    private Animator _animator;
    private PlayerUI _playerUI;
    private int _moveHash_Bool;
    private int _fireHash_Bool;
    private int _reloadHash_Trigger;

    private IInteractable _cachedInteractable;


    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        _currentGun = _guns.Find(x => x.GunData.GunID == _currentGunID);
        _playerUI = GetComponent<PlayerUI>();
        _audioSource = GetComponent<AudioSource>();
        _navAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _moveHash_Bool = Animator.StringToHash("Move");
        _fireHash_Bool = Animator.StringToHash("Fire");
        _reloadHash_Trigger = Animator.StringToHash("Reload");

        _currentGun.ReloadWeapon();
        _playerUI.UpdateCurrentAmmo(_currentGun.CurrentAmmo, _currentGun.GunData.ClipSize);
    }

    private void Update()
    {
        HandleInteractionInput();
        HandleMovementInput();
        HandleFiringInput();
        HandleReloadInput();
    }

    private void HandleInteractionInput()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, _interactionDistance, _interactionLayerMask))
        {
            if(_cachedInteractable == null)
            {
                _cachedInteractable = hit.transform.GetComponent<IInteractable>();
            }

            _playerUI.UpdateInteractionText(_cachedInteractable.GetInteractionMessage().message);

            if(_cachedInteractable.GetInteractionMessage().type == INTERACTABLE_TYPE.DUMPSTER)
            {
                _cachedInteractable.SetCanInteract(_hasBody);

                if(_hasBody)
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        _cachedInteractable.Interact();
                        _bodyBag.SetActive(false);
                        _hasBody = false;
                    }
                }
            }
            else if(_cachedInteractable.GetInteractionMessage().type == INTERACTABLE_TYPE.ENEMY_BODY)
            {
                _cachedInteractable.SetCanInteract(!_hasBody);

                if (!_hasBody)
                {
                    if(Input.GetKeyDown(KeyCode.E))
                    {
                        _cachedInteractable.Interact();
                        _hasBody = true;
                        _bodyBag.SetActive(true);
                    }
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    _cachedInteractable.Interact();
                }
            }
        }
        else
        {
            if(_cachedInteractable != null)
            {
                _cachedInteractable = null;
                _playerUI.UpdateInteractionText("");
            }
        }
    }

    private void HandleMovementInput()
    {
        if (Input.GetKey(KeyCode.W))
        {
            NavMeshPath navMeshPath = new NavMeshPath();

            if (_navAgent.CalculatePath(_moveLoc.position, navMeshPath) && navMeshPath.status == NavMeshPathStatus.PathComplete)
            {
                _navAgent.isStopped = false;
                _animator.SetBool(_moveHash_Bool, true);
                _navAgent.SetPath(navMeshPath);
            }
        }
        else
        {
            _navAgent.isStopped = true;
            _animator.SetBool(_moveHash_Bool, false);
        }

        float rotationX = Input.GetAxis("Mouse X") * _rotationSpeed * Mathf.Deg2Rad;
        transform.Rotate(Vector3.up, rotationX);
    }

    private void HandleReloadInput()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartReloading();
        }
    }

    private void StartReloading()
    {
        if (!_currentGun.IsReloading)
        {
            _currentGun.SetIsReloading(true);
            _animator.SetTrigger(_reloadHash_Trigger);
        }
    }

    private void HandleFiringInput()
    {
        if(Input.GetMouseButton(0))
        {
            if(_currentGun.CurrentAmmo > 0)
            {
                _animator.SetBool(_fireHash_Bool, true);
            }
            else
            {
                _animator.SetBool(_fireHash_Bool, false);
                StartReloading();
            }
        }
        else
        {
            _animator.SetBool(_fireHash_Bool, false);
        }
    }

    //ANIMATION EVENT
    private void Fire()
    {
        if (_currentGun.CurrentAmmo <= 0) return;

        _playerUI.UpdateCurrentAmmo(_currentGun.CurrentAmmo, _currentGun.GunData.ClipSize);
        _currentGun.DecrementCurrentAmmo();

        _audioSource.PlayOneShot(_currentGun.GunData.ShootingSounds[Random.Range(0, _currentGun.GunData.ShootingSounds.Count)]);

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 100.0f, _shootingLayer))
        {
            IDamageable damageable = hit.transform.GetComponent<IDamageable>();
            if(damageable != null) damageable.TakeDamage(_currentGun.GunData.Damage);

            GameObject tempEffect = Instantiate(_bloodHit, hit.point, Quaternion.identity);
            Destroy(tempEffect, 1f);
        }
    }

    //ANIMATION EVENT
    private void ReloadStarted()
    {
        _audioSource.PlayOneShot(_currentGun.GunData.ReloadingSounds[Random.Range(0, _currentGun.GunData.ReloadingSounds.Count)]);
    }

    //ANIMATION EVENT
    private void ReloadFinished()
    {
        _currentGun.ReloadWeapon();
        _currentGun.SetIsReloading(false);
        _playerUI.UpdateCurrentAmmo(_currentGun.CurrentAmmo, _currentGun.GunData.ClipSize);
        Debug.Log("Reload finished");
    }

    public void OnDetect()
    {
        Debug.Log("PLAYER DETECTED!");
    }
}
