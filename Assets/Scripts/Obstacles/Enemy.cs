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
    [SerializeField] private float _currentSpeed;
    [SerializeField] private int _damage = 2;

    private Rigidbody _rb;
    private Animator _animator;
    private Collider _collider;
    private AudioSource _soundOfDeath;

    private int _mpd = 20;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _collider = GetComponent<Collider>();
        _soundOfDeath = GetComponent<AudioSource>();
    }

    private void Update()
    {
        _rb.AddTorque(transform.position += transform.forward * _currentSpeed * Time.deltaTime);
    }

    public override Transform AnchorPoint
    {
        get { return _anchorPoint; }
    }

    public override int MinimumPossibleDistance 
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
        Coin giantCoin = Instantiate(_giantCoinPrefab, transform, true);
        giantCoin.transform.position = transform.position;

        _animator.SetTrigger("Death");
        _currentSpeed = 0;

        _rb.isKinematic = true;
        _collider.enabled = false;

        _soundOfDeath.Play();
        Destroy(this.gameObject, 2.5f);
    }
}
