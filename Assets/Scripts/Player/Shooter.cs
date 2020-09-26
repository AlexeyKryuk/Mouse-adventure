using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Shooter : MonoBehaviour
{
    [SerializeField] private Snowball _snowballPrefab;
    [SerializeField] private Transform _arrowPoint;
    [SerializeField] private static int _snowNumber = 10;
    [SerializeField] private Animator _mouseAnimator;
    [SerializeField] private AudioSource _soundOfThrow;

    public static int SnowNumber => _snowNumber;

    public event UnityAction SnowballThrown;
    public event UnityAction EnemyKilled;

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
        _mouseAnimator.SetTrigger("Attack");
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
        EnemyKilled?.Invoke();
    }
}
