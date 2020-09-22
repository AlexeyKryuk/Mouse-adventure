using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkPlacerRunner : MonoBehaviour
{
    [SerializeField] private Transform _player;

    [SerializeField] private Chunk _сhunkPrefab;
    [SerializeField] private Chunk _firstChunk;

    [SerializeField] private int _distance;

    private List<Chunk> _spawnedChunks = new List<Chunk>();

    void Start()
    {
        _spawnedChunks.Add(_firstChunk);
    }

    private void Update()
    {
        if (_player.position.z > _spawnedChunks[_spawnedChunks.Count - 1].End.position.z - _distance)
        {
            SpawnedChunk();
        }
        if (_player.position.z > _spawnedChunks[0].End.position.z + _distance / 3)
        {
            Destroy(_spawnedChunks[0].gameObject);
            _spawnedChunks.RemoveAt(0);
        }
    }

    private void SpawnedChunk()
    {
        Chunk newChunk = Instantiate(_сhunkPrefab);
        newChunk.transform.position = _spawnedChunks[_spawnedChunks.Count - 1].End.position - newChunk.Begin.localPosition;

        _spawnedChunks.Add(newChunk);
    }

    private void SpawnedChunk(Chunk chunk)
    {
        Chunk newChunk = Instantiate(chunk);
        newChunk.transform.position = _spawnedChunks[_spawnedChunks.Count - 1].End.position - newChunk.Begin.localPosition;
    }
}
