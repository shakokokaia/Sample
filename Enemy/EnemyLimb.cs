using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLimb : MonoBehaviour, IDamageable
{
    [SerializeField] float _damageModifier;

    private Enemy _enemy;

    private void Awake()
    {
        _enemy = GetComponentInParent<Enemy>();
    }
    public void TakeDamage(float damage)
    {
        _enemy.TakeDamage(damage * _damageModifier);
    }
}
