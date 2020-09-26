using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinView : PointView
{
    protected override void OnPointPickedUp(Point point, int count)
    {
        if (point.TryGetComponent(out Coin coin))
        {
            _countOfPoint += count;
            _countOfPointText.text = _countOfPoint.ToString();

            _soundOfPoint.Play();
        }
    }
}
