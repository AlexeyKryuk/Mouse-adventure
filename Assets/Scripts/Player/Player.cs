using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Shooter))]
public class Player : MonoBehaviour
{
    [SerializeField] private int _health = 10;
    [SerializeField] private List<WolfMovement> _wolfMovements;
    [SerializeField] private ObjectTracking _sled;
    [SerializeField] private Wolf _wolf;

    private Shooter _shooter;

    private int _countOfCandies;
    private int _countOfCoins;
    private int _countOfKilledEnemies;

    private Animator _animator;

    public bool GodMod { get; private set; }
    public int CountOfCandies => _countOfCandies;
    public int CountOfCoins => _countOfCoins;
    public int CountOfKilledEnemies => _countOfKilledEnemies;

    public event UnityAction Died;
    public event UnityAction EnemyKilled;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _shooter = GetComponent<Shooter>();
    }

    private void OnEnable()
    {
        _wolf.TookDamage += OnTookDamage;
        _wolf.PickedUpPoint += OnPickedUpPoint;
        _shooter.EnemyKilled += OnEnemyKilled;
    }

    private void OnDisable()
    {
        _wolf.TookDamage -= OnTookDamage;
        _wolf.PickedUpPoint -= OnPickedUpPoint;
        _shooter.EnemyKilled -= OnEnemyKilled;
    }

    private void OnTookDamage(int damage, string obstacleName)
    {
        _health -= damage;
        _animator.SetTrigger(obstacleName);

        if (_health <= 0)
        {
            Die();
        }
    }

    private void OnPickedUpPoint(Point point, int value)
    {
        if (point.TryGetComponent(out Candy candy))
        {
            _countOfCandies += value;
        }
        if (point.TryGetComponent(out Coin coin))
        {
            _countOfCoins += value;
        }
        if (point.TryGetComponent(out Heart heart))
        {
            Heal(value);
        }
    }

    public void OnEnemyKilled()
    {
        EnemyKilled?.Invoke();
    }

    private void Heal(int value)
    {
        if (_health + value > 10)
            _health = 10;
        else
            _health += value;
    }

    private void StopMovement()
    {
        foreach (WolfMovement wolf in _wolfMovements)
        {
            wolf.enabled = false;
        }

        _sled.enabled = false;
    }

    private void Die()
    {
        Died?.Invoke();

        StopMovement();

        this.enabled = false;
    }
}
