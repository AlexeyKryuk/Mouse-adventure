using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System;
using UnityEngine.Events;

public class Wolf : MonoBehaviour
{
    public event UnityAction<Point, int> PickedUpPoint;
    public event UnityAction<int, string> TookDamage;

    public void Collide(int damage, string obstacleName)
    {
        TookDamage?.Invoke(damage, obstacleName);
    }

    public void PickUpPoint(Point point, int value)
    {
        PickedUpPoint?.Invoke(point, value);
    }
}
