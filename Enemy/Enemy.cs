using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] float _maxHealth;
    [SerializeField] int _interactableLayerIndex;

    private List<Rigidbody> _ragdollBodies = new List<Rigidbody>();
    private List<EnemyBody> _bodyBagColliders = new List<EnemyBody>();
    private Animator _animator;
    private NavMeshAgent _navAgent;
    private float _currentHealth;
    private bool _isDead;

    private void Awake()
    {
        _currentHealth = _maxHealth;

        _animator = GetComponent<Animator>();
        _navAgent = GetComponent<NavMeshAgent>();

        _ragdollBodies = transform.GetComponentsInChildren<Rigidbody>().ToList();
        _bodyBagColliders = transform.GetComponentsInChildren<EnemyBody>().ToList();

        SetRagdollActive(false);

        foreach (EnemyBody item in _bodyBagColliders)
        {
            item.onInteract = () => { gameObject.SetActive(false); };
        }
    }

    public void TakeDamage(float value)
    {
        if (_isDead) return;

        _currentHealth -= value;
        if(_currentHealth <= 0)
        {
            Die();
        }
    }

    private void SetRagdollActive(bool val)
    {
        foreach (Rigidbody item in _ragdollBodies)
        {
            item.isKinematic = !val;
        }
    }

    private void Die()
    {
        _isDead = true;
        _animator.enabled = false;
        _navAgent.isStopped = true;
        _navAgent.enabled = false;

        SetRagdollActive(true);
        foreach (EnemyBody item in _bodyBagColliders)
        {
            item.gameObject.layer = _interactableLayerIndex;
        }
    }
}
