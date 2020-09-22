using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Snowball : MonoBehaviour
{
    private float _speed = 20f;
    private float _lifetime = 3f;
    private float _currentLifetime = 0f;
    private Shooter _shooter;

    public event UnityAction EnemyKilled;

    private void FixedUpdate()
    {
        Move();

        _currentLifetime += Time.fixedDeltaTime;
        if (_currentLifetime > _lifetime)
        {
            EnemyKilled -= _shooter.OnEnemyKilled;
            Destroy(gameObject);
        }
    }

    public void Init(Shooter shooter)
    {
        _shooter = shooter;
    }

    private void Move()
    {
        transform.Translate(0, 0, _speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            enemy.Die();
            EnemyKilled?.Invoke();
            EnemyKilled -= _shooter.OnEnemyKilled;
        }

        Destroy(gameObject);
    }

    public void IncreaseSpeed(float value)
    {
        _speed += value;
    }
}
