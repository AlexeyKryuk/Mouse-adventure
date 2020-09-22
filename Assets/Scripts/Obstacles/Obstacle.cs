﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Obstacle : MonoBehaviour
{
    public abstract int MinimumPossibleDistance { get; protected set; }

    public abstract Transform AnchorPoint { get; }

    protected abstract void CollisionReaction(Collision collision);

    private void OnCollisionEnter(Collision collision)
    {
        CollisionReaction(collision);
    }
}
