using Assets.Scripts.Spawner;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : ObjectPool
{
    [SerializeField] private Obstacle[] _obstaclePrefabs;
    [SerializeField] private Transform[] _anchors;

    private Obstacle _previousObstacle;
    private Obstacle[] _obstacles;
    private int _numberOfStripes = 3;

    public Obstacle[] Obstacles => _obstacles;

    private void Awake()
    {
        _obstacles = new Obstacle[_numberOfStripes];

        for (int i = 0; i < _numberOfStripes; i++)
        {
            Initialize(_obstaclePrefabs, _anchors[i], Quaternion.identity);
        }
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
        for (int i = 0; i < _numberOfStripes; i++)
        {
            if (TryGetRandomObject(out SpawnObject spawnObject, _anchors[i]))
            {
                SetObject(spawnObject.GetComponent<Obstacle>(), i);
            }
        }
    }

    private void SetObject(Obstacle obstacle, int stripNumber)
    {
        Vector3 deltaZ = Vector3.zero;

        if (stripNumber == 0)
        {
            float minDistance = -5f, maxDistance = 5f;

            deltaZ = new Vector3(0f, 0f, Random.Range(minDistance, maxDistance));
        }
        else
        {
            if (_previousObstacle.transform.localPosition.z < 0)
            {
                deltaZ = new Vector3(0f, 0f, _previousObstacle.transform.localPosition.z + _previousObstacle.MinimumPossibleDistance);
            }
            else
            {
                deltaZ = new Vector3(0f, 0f, _previousObstacle.transform.localPosition.z - _previousObstacle.MinimumPossibleDistance);
            }    
        }

        obstacle.transform.localPosition = deltaZ;
        obstacle.gameObject.SetActive(true);

        _obstacles[stripNumber] = obstacle;
        _previousObstacle = obstacle;
    }

    private void RemoveObjects()
    {
        for (int i = 0; i < _obstacles.Length; i++)
        {
            _obstacles[i].gameObject.SetActive(false);
        }
    }

}
