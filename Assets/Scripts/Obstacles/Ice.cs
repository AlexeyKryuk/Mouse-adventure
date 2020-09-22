using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(MeshRenderer))]
public class Ice : Obstacle
{
    [SerializeField] private ParticleSystem _iceEffect;
    [SerializeField] private Material _crackedIce;
    [SerializeField] private Material _defaultIce;
    [SerializeField] private Transform _anchorPoint;
    [SerializeField] private int _damage = 1;

    private Material[] _materials;
    private AudioSource _audio;

    private float _startTime;
    private int _mpd = 14;
    private float delay = 1.5f;

    private void Awake()
    {
        _materials = GetComponent<MeshRenderer>().materials;
        _audio = GetComponent<AudioSource>();
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
            _startTime = Time.time;
            _audio.Play();
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Wolf wolf))
        {
            TryToKill(wolf);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Wolf wolf))
        {
            ChangeMaterial(_defaultIce);
            _audio.Stop();
        }
    }

    private void TryToKill(Wolf wolf)
    {
        ChangeMaterial(_crackedIce);

        if (Time.time > _startTime + delay)
        {
            wolf.Collide(_damage, GetType().Name);
            _iceEffect.Play();
            _startTime = Time.time;
        }
    }

    private void ChangeMaterial(Material material)
    {
        _materials[0] = material;
        GetComponent<MeshRenderer>().materials = _materials;
    }
}
