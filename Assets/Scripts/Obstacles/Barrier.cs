using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Barrier : Obstacle
{
    [SerializeField] private Transform _anchorPoint;
    [SerializeField] private int _damage = 3;

    private Collider _collider;
    private int _mpd = 8;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
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

            _collider.enabled = false;
        }
    }
}
