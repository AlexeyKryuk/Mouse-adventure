using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelthBar : MonoBehaviour
{
    [SerializeField] private Wolf _wolf;
    [SerializeField] private Slider _slider;
    [SerializeField] private Animator _animator;

    private void OnEnable()
    {
        _wolf.TookDamage += OnTookDamage;
        _wolf.PickedUpPoint += OnHealthPointPickedUp;
    }

    private void OnDisable()
    {
        _wolf.TookDamage -= OnTookDamage;
        _wolf.PickedUpPoint += OnHealthPointPickedUp;
    }

    private void OnTookDamage(int value, string obstacleName)
    {
        _slider.value -= value;
    }

    private void OnHealthPointPickedUp(Point point, int value)
    {
        if (point.TryGetComponent(out Heart heart))
        {
            _slider.value += value;
            _animator.SetTrigger("Heal");
        }
    }
}
