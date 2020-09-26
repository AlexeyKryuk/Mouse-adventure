using Assets.Scripts.Spawner;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkSpawner : ObjectPool
{
    [SerializeField] private Transform _player;
    [SerializeField] private int _distance;
    [SerializeField] private SpawnObject _сhunkPrefab;
    [SerializeField] private SpawnObject _firstChunk;

    private SpawnObject _previuosChunk;
    private SpawnObject _currentChunk;

    private void Awake()
    {
        Initialize(_сhunkPrefab);

        _currentChunk = _firstChunk;
        _previuosChunk = _firstChunk;
    }

    private void Update()
    {
        if (_player.position.z > _currentChunk.End.position.z - _distance)
        {
            SpawnChunk();
        }
        else if (_player.position.z > _previuosChunk.End.position.z + _distance / 3)
        {
            RemoveChunk(_previuosChunk);
        }
    }

    private void SpawnChunk()
    {
        if (TryGetObject(out SpawnObject chunk))
        {
            _previuosChunk = _currentChunk;

            SetChunk(chunk);

            _currentChunk = chunk;
        }
    }

    private void RemoveChunk(SpawnObject chunk)
    {
        chunk.gameObject.SetActive(false);
    }

    private void SetChunk(SpawnObject chunk)
    {
        chunk.transform.position = _currentChunk.End.position - chunk.Begin.localPosition;
        chunk.gameObject.SetActive(true);
    }
}
