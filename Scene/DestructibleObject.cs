using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObject : MonoBehaviour, IDamageable
{
    [SerializeField] float _maxHealth;

    private float _currentHealth;
    private bool _isDestroyed;

    private void Awake()
    {
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(float damage)
    {
        if (_isDestroyed) return;

        _currentHealth -= damage;
        if(_currentHealth <= 0)
        {
            Destruct();
        }
    }

    private void Destruct()
    {
        _isDestroyed = true;

        Destroy(gameObject);
    }
}
