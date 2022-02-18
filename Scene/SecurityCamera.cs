using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SecurityCamera : MonoBehaviour, IDamageable
{
    [SerializeField] float _rotationTime;
    [SerializeField] Vector3 _leftMaxRotation;
    [SerializeField] Vector3 _rightMaxRotation;
    [SerializeField] GameObject _destroyedVFX;
    [SerializeField] Transform _cameraHead;
    [SerializeField] VisionMesh _visionMesh;

    private bool _isActive = true;

    private void Start()
    {
        _visionMesh.gameObject.SetActive(true);
        _visionMesh.onDetected = OnDetectTarget;
        _visionMesh.onSeen = OnSeenTarget;
        RotateCamera();
    }

    private void RotateCamera()
    {
        _cameraHead.DORotate(_rightMaxRotation, _rotationTime).OnComplete(() => 
        { 
            _cameraHead.DORotate(_leftMaxRotation, _rotationTime).OnComplete(()=> 
            { 
                RotateCamera(); 
            }); 
        });
    }

    private void OnSeenTarget(IDetectable detectable)
    {
        if (!_isActive) return;

        Debug.Log("Seen target!");
    }

    private void OnDetectTarget(IDetectable detectable)
    {
        if (!_isActive) return;

        detectable.OnDetect();
    }

    public void TakeDamage(float damage)
    {
        if (!_isActive) return;
        else
        {
            Destroy();
        }
    }

    private void Destroy()
    {
        _cameraHead.DOKill();
        _visionMesh.gameObject.SetActive(false);
        _isActive = false;
        _destroyedVFX.SetActive(true);
    }
}
