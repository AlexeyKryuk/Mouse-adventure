using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    [SerializeField] private Transform _begin;
    [SerializeField] private Transform _end;

    public Transform Begin => _begin;
    public Transform End => _end;
}
