using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class PointView : MonoBehaviour
{
    [SerializeField] private Wolf _wolf;
    [SerializeField] protected Text _countOfPointText;
    [SerializeField] protected AudioSource _soundOfPoint;

    protected int _countOfPoint = 0;

    protected void OnEnable()
    {
        _wolf.PickedUpPoint += OnPointPickedUp;
    }

    protected void OnDisable()
    {
        _wolf.PickedUpPoint -= OnPointPickedUp;
    }

    protected abstract void OnPointPickedUp(Point point, int count);
}
