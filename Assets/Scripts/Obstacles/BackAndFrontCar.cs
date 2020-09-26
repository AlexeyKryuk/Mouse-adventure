using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackAndFrontCar : Obstacle
{
    private int _damage = 10;

    public override float MinimumPossibleDistance { get; protected set; }

    public override Transform AnchorPoint { get; }

    protected override void CollisionReaction(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Wolf wolf))
        {
            wolf.Collide(_damage, "Barrier");
        }
    }
}
