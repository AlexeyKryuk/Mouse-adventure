using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

abstract public class Point : MonoBehaviour
{
    protected abstract void TriggerReaction(Wolf wolf);

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Wolf wolf))
            TriggerReaction(wolf);
    }
}
