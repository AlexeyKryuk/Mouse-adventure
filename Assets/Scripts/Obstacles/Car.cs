using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(MeshRenderer))]
public class Car : Obstacle
{
    [SerializeField] private Transform _anchorPoint;
    [SerializeField] private Collider _back;
    [SerializeField] private Collider _front;
    [SerializeField] private Material[] _materials;
    [SerializeField] private int _damage = 3;

    private Collider _collider;

    private int _mpd = 10;

    private void Awake()
    {
        GetComponent<MeshRenderer>().material = _materials[UnityEngine.Random.Range(0, _materials.Length)];
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
            wolf.Collide(_damage, "Barrier");

            _collider.enabled = false;
            _back.enabled = false;
            _front.enabled = false;
        }
    }
}
