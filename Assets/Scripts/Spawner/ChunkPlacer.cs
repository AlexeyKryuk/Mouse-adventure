using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkPlacer : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private Chunk _сhunkPrefab;
    [SerializeField] private Chunk _firstChunk;
    [SerializeField] private Chunk _lastChunk;
    [SerializeField] private int _distance;
    [SerializeField] private float _playTime = 10f;

    private List<Chunk> _spawnedChunks = new List<Chunk>();
    private float _currentTime;

    void Start()
    {
        _currentTime = _playTime;
        _spawnedChunks.Add(_firstChunk);
    }

    private void Update()
    {
        _currentTime -= Time.deltaTime;
        if (_player.position.z > _spawnedChunks[_spawnedChunks.Count - 1].End.position.z - _distance)
        {
            if (_currentTime >= 0)
                SpawnedChunk();
            else
            {
                SpawnedChunk(_lastChunk);
                gameObject.SetActive(false);
            }
        }
        if (_player.position.z  > _spawnedChunks[0].End.position.z + _distance / 3)
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