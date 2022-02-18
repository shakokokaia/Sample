using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeTrapTrigger : MonoBehaviour, IDamageable
{
    public System.Action onDestroyed;

    [SerializeField] float _maxHealth;

    private float _currentHealth;
    private bool _isDestroyed;

    private void Awake()
    {
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;

        if(_currentHealth <= 0)
        {
            Destruct();
        }
    }

    private void Destruct()
    {
        if (_isDestroyed) return;

        _isDestroyed = true;
        onDestroyed?.Invoke();
        Destroy(gameObject);
    }
}
