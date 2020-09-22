using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Heart : Point
{
    [SerializeField] private int _healthPoints = 5;

    protected override void TriggerReaction(Wolf wolf)
    {
        wolf.PickUpPoint(this, _healthPoints);
        Destroy(gameObject);
    }
}
