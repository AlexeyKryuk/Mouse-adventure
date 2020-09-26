using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(AudioSource))]
public class Enemy : Obstacle
{
    [SerializeField] private Transform _anchorPoint;
    [SerializeField] private Coin _giantCoinPrefab;
    [SerializeField] private float _speed;
    [SerializeField] private int _damage = 2;

    private float _currentSpeed;
    private Rigidbody _rb;
    private Animator _animator;
    private Collider _collider;
    private AudioSource _soundOfDeath;

    private float _mpd = 1;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _collider = GetComponent<Collider>();
        _soundOfDeath = GetComponent<AudioSource>();

        transform.Rotate(Vector3.up * 180f);
    }

    private void OnEnable()
    {
        _currentSpeed = _speed;
        _collider.enabled = true;
    }

    private void Update()
    {
        _rb.AddTorque(transform.position += transform.forward * _currentSpeed * Time.deltaTime);
    }

    public override Transform AnchorPoint
    {
        get { return _anchorPoint; }
    }

    public override float MinimumPossibleDistance 
    { 
        get => _mpd; 
        protected set => _mpd = value; 
    }

    protected override void CollisionReaction(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Wolf wolf))
        {
            wolf.Collide(_damage, GetType().Name);
            Die();
        }
        else if (collision.gameObject.TryGetComponent(out Obstacle obstacle))
        {
            if (!obstacle.TryGetComponent(out Ice ice))
            {
                Physics.IgnoreCollision(_collider, collision.collider, true);

                _animator.SetTrigger("Jump");
            }
        }
    }

    public void Die()
    {
        Coin giantCoin = Instantiate(_giantCoinPrefab);
        giantCoin.transform.position = transform.position;

        _animator.SetTrigger("Death");
        _currentSpeed = 0;

        _rb.isKinematic = true;
        _collider.enabled = false;

        _soundOfDeath.Play();

        Invoke("TurnOff", 0.5f);
    }

    private void TurnOff()
    {
        gameObject.SetActive(false);
    }
}
