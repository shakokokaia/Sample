using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerBullet : MonoBehaviour
{
    private System.Action<Transform> _onEnemyHit;

    private void Awake()
    {
        Destroy(gameObject, 0.5f);
    }

    public void Move(Vector3 position, System.Action<Transform> onEnemyHit)
    {
        _onEnemyHit = onEnemyHit;
        transform.DOMove(position, 0.2f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            _onEnemyHit?.Invoke(other.transform);
        }
    }
}
