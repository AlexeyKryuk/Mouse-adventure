using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CooldownBar : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private Shooter _shooter;
    [SerializeField] private Wolf _wolf;

    private void OnEnable()
    {
        _shooter.SnowballThrown += OnSnowballThrown;
        _wolf.PickedUpPoint += OnSnowballPickedUp;
    }

    private void OnDisable()
    {
        _shooter.SnowballThrown -= OnSnowballThrown;
        _wolf.PickedUpPoint -= OnSnowballPickedUp;
    }

    private void OnSnowballThrown()
    {
        _slider.value--;
    }

    private void OnSnowballPickedUp(Point point, int count)
    {
        if (TryGetComponent(out SnowballPoint snowballPoint))
        {
            _slider.value += count;
        }
    }
}
