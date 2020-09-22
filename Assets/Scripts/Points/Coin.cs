using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Coin : Point
{
    [SerializeField] private bool _isGiant;
    [SerializeField] private int _costMiniCoin = 1;
    [SerializeField] private int _costGiantCoin = 5;

    protected override void TriggerReaction(Wolf wolf)
    {
        if (_isGiant)
        {
            wolf.PickUpPoint(this, _costGiantCoin);
        }
        else
        {
            wolf.PickUpPoint(this, _costMiniCoin);
        }

        Destroy(gameObject);
    }
}
