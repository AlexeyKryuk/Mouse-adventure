using Assets.Scripts.Spawner;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpawnObject))]
public class EnviromentSpawner : ObjectPool
{
    [SerializeField] private SpawnObject[] _housePrefabs;
    [SerializeField] private Transform _leftSide;
    [SerializeField] private Transform _rightSide;

    private SpawnObject _spawnObject;
    private SpawnObject _leftSpawnObject, _rightSpawnObject;

    private void Awake()
    {
        _spawnObject = GetComponent<SpawnObject>();

        Initialize(_housePrefabs, _leftSide, Quaternion.identity);
        Initialize(_housePrefabs, _rightSide, Quaternion.AngleAxis(180f, Vector3.up));
    }

    private void OnEnable()
    {
        SpawnObjects();
    }

    private void OnDisable()
    {
        RemoveObjects();
    }

    private void SpawnObjects()
    {
        if (TryGetRandomObject(out SpawnObject spawnObject1, _leftSide))
        {
            _leftSpawnObject = spawnObject1;
        }
        if (TryGetRandomObject(out SpawnObject spawnObject2, _rightSide))
        {
            _rightSpawnObject = spawnObject2;
        }

        SetObject();
    }
    
    private void SetObject()
    {
        Vector3 _startPoint = _spawnObject.Begin.position;

        _leftSpawnObject.transform.position = _startPoint - _leftSpawnObject.Begin.localPosition;
        _leftSpawnObject.gameObject.SetActive(true);

        _rightSpawnObject.transform.position = _startPoint + _rightSpawnObject.End.localPosition;
        _rightSpawnObject.gameObject.SetActive(true);
    }

    private void RemoveObjects()
    {
        _leftSpawnObject.gameObject.SetActive(false);
        _rightSpawnObject.gameObject.SetActive(false);
    }
}
