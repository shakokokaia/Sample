using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.AI;

public class BladeTrap : MonoBehaviour
{
    [SerializeField] string _NOTE_ = "Add TwinY layer to objects which are too close";

    [SerializeField] float _damageOnImpact;
    [SerializeField] float _endRotationY;
    [SerializeField] Transform _centerOfMass;
    [SerializeField] BladeTrapTrigger _bladeTrapTrigger;
    [SerializeField] NavMeshObstacle _navMeshObstacle;
    [SerializeField] List<Transform> _ignoreObjects = new List<Transform>();

    private Rigidbody _rigidbody;
    private bool _enabled;
    Vector3 m_EulerAngleVelocity;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.centerOfMass = _centerOfMass.transform.localPosition;
        _bladeTrapTrigger.onDestroyed = Activate;
        m_EulerAngleVelocity = new Vector3(-200, 0, 0);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!_enabled) return;

        if (!_ignoreObjects.Contains(other.transform))
        {
            IDamageable damageable = other.transform.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(_damageOnImpact);
            }
        }
    }

    private void Update()
    {
        if (_enabled)
        {

            float currentRotationY = transform.eulerAngles.y > 180f ? transform.eulerAngles.y - 360f : transform.eulerAngles.y;

            if ((currentRotationY > _endRotationY && currentRotationY < _endRotationY + 5f) || (currentRotationY < _endRotationY && currentRotationY > _endRotationY - 5f))
            {
                _navMeshObstacle.enabled = true;
                _enabled = false;
                _rigidbody.isKinematic = true;
            }
        }
    }

    private void FixedUpdate()
    {
        if (_enabled)
        {
            Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocity * Time.fixedDeltaTime);
            _rigidbody.MoveRotation(_rigidbody.rotation * deltaRotation);
        }

    }

    private void Activate()
    {
        _enabled = true;
    }
}
