using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Candy : Point
{
    [SerializeField] private int _cost = 1;

    protected override void TriggerReaction(Wolf wolf)
    {
        wolf.PickUpPoint(this, _cost);
        Destroy(gameObject);
    }
}
