using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class Shooter : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Snowball _snowballPrefab;
    [SerializeField] private Transform _arrowPoint;
    [SerializeField] private static int _snowNumber;

    private Animator _animator;
    private AudioSource _soundOfThrow;

    public static int SnowNumber => _snowNumber;

    public event UnityAction SnowballThrown;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _soundOfThrow = GetComponent<AudioSource>();
    }

    private void Start()
    {
        _snowNumber = 10;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (SnowNumber > 0)
            {
                ThrowSnowBall();
            }
        }
    }

    private void ThrowSnowBall()
    {
        _animator.SetTrigger("Attack");
        _soundOfThrow.Play();

        InitSnowball();

        _snowNumber--;
    }

    private void InitSnowball()
    {
        SnowballThrown?.Invoke();

        var snowball = Instantiate(_snowballPrefab) as Snowball;
        snowball.transform.position = _arrowPoint.position;

        snowball.EnemyKilled += OnEnemyKilled;
        snowball.Init(this);
    }

    public void OnEnemyKilled()
    {
        _player.OnEnemyKilled();
    }
}
