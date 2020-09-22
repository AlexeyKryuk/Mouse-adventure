using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class WolfMovement : MonoBehaviour
{
    [SerializeField] private ObjectTracking _sledSpeed;
    [SerializeField] private Snowball _snowballSpeed;
    [SerializeField] private float _firstLanePos, _laneDistance, _sideSpeed;

    private float _currentSpeed = 7;

    private Rigidbody _rb;
    private Animator _animator;
    private AudioSource _sound;

    private int _laneNumber = 1;
    private int _lanesCount = 2;

    private float timeAfterLastAcceleration;
    private float accelerationInterval = 8f;

    private void OnEnable()
    {
        _animator.SetBool("Run", true);
        _sound.Play();
    }

    private void OnDisable()
    {
        _animator.SetBool("Sit", true);
        _sound.Stop();
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _sound = GetComponent<AudioSource>();
    }

    private void Update()
    {
        Displacement();

        if (_currentSpeed < 20f)
            Acceleration();
    }

    private void FixedUpdate()
    {
        _rb.AddTorque(transform.position += transform.forward * _currentSpeed * Time.deltaTime);
    }

    private void Acceleration()
    {
        timeAfterLastAcceleration += Time.deltaTime;

        if (timeAfterLastAcceleration >= accelerationInterval)
        {
            _currentSpeed += 0.5f;
            _sledSpeed.IncreaseDistanceFactor(0.1f);
            _snowballSpeed.IncreaseSpeed(0.2f);

            timeAfterLastAcceleration = 0f;
        }
    }

    private void Displacement()
    {
        CheckInput();

        Vector3 newPos = transform.position;
        newPos.x = Mathf.Lerp(newPos.x, _firstLanePos + (_laneNumber * _laneDistance), Time.deltaTime * _sideSpeed);

        transform.position = newPos;
    }

    private void CheckInput()
    {
        int sign = 0;

        if (Input.GetKeyDown(KeyCode.A))
            sign = -1;
        else if (Input.GetKeyDown(KeyCode.D))
            sign = 1;
        else return;

        _laneNumber += sign;
        _laneNumber = Mathf.Clamp(_laneNumber, 0, _lanesCount);

    }
}
