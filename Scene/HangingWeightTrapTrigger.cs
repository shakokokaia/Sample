using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HangingWeightTrapTrigger : MonoBehaviour, IDamageable
{
    [SerializeField] float _maxHealth;
    [SerializeField] HangingWeightTrap _hangingWeightTrap;

    private bool _isDestroyed;
    private float _currentHealth;

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
        _hangingWeightTrap.Activate();
        Destroy(gameObject);
    }
}
