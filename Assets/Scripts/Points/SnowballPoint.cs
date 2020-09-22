using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SnowballPoint : Point
{
    [SerializeField] private int _snowballPoints = 1;

    protected override void TriggerReaction(Wolf wolf)
    {
        wolf.PickUpPoint(this, _snowballPoints);
        Destroy(gameObject);
    }
}
