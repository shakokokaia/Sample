using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HangingWeightTrap : MonoBehaviour
{
    [SerializeField] float _damageOnImpact;
    [SerializeField] List<Transform> _ignoreObjects = new List<Transform>();

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Activate()
    {
        _rigidbody.isKinematic = false;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!_ignoreObjects.Contains(other.transform) && _rigidbody.velocity.magnitude > 1f)
        {
            IDamageable damageable = other.transform.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(_damageOnImpact);
            }
        }
    }
}
