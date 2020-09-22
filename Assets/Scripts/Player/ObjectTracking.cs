using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ObjectTracking : MonoBehaviour
{
    [SerializeField] private GameObject _target;

    private float _distanceFactor = 3.6f;
    private float _rotSpeed = 50f;
    private float _speed;
    private Rigidbody _rb;

    public GameObject Target => _target;
    public float DistanceFactor => _distanceFactor;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Vector3 look = Target.transform.position - transform.position;

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(look), _rotSpeed * Time.deltaTime);

        _speed = Vector3.Distance(transform.position, Target.transform.position) * _distanceFactor;
    }

    private void FixedUpdate()
    {
        _rb.AddTorque(transform.position += transform.forward * (_speed * Time.deltaTime));
    }

    public void IncreaseDistanceFactor(float value)
    {
        _distanceFactor += value;
    }
}
