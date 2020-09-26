﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Car : Obstacle
{
    [SerializeField] private Transform _anchorPoint;
    [SerializeField] private Collider _back;
    [SerializeField] private Collider _front;
    [SerializeField] private int _damage = 3;

    private Collider _collider;

    private float _mpd = 3;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
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
            wolf.Collide(_damage, "Barrier");

            _collider.enabled = false;
            _back.enabled = false;
            _front.enabled = false;
        }
    }
}
