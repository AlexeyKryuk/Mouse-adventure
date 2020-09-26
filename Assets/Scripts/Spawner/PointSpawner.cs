using Assets.Scripts.Spawner;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ObstacleSpawner))]
[RequireComponent(typeof(SpawnObject))]
public class PointSpawner : ObjectPool
{
    [SerializeField] private Coin _coinPrefab;
    [SerializeField] private SnowballPoint _snowballPrefab;
    [SerializeField] private Heart _heartPrefab;
    [SerializeField] private Candy[] _candyPrefabs;

    private List<Point> _spawnedPoints = new List<Point>();
    private ObstacleSpawner _obstacleSpawner;
    private SpawnObject _spawnObject;
    private int _numberOfStripes = 3;
    private int _distanceBetweenPoints = 3;

    private void Awake()
    {
        _obstacleSpawner = GetComponent<ObstacleSpawner>();
        _spawnObject = GetComponent<SpawnObject>();
    }

    private void OnEnable()
    {
        GeneratePoints();
    }

    private void OnDisable()
    {
        foreach (var point in _spawnedPoints)
        {
            Destroy(point);
        }

        _spawnedPoints.RemoveRange(0, _spawnedPoints.Count);
    }

    private void GeneratePoints()
    {
        int S0, S1, delta, maxNumberOfPoints, numberOfPoints, typeOfPoint;
        Coin coin = null;
        SnowballPoint snowball = null;
        Heart heart = null;
        Candy candy = null;

        for (int i = 0; i < _numberOfStripes; i++)
        {
            S0 = (int)Vector3.Distance(_obstacleSpawner.Obstacles[i].AnchorPoint.position, _spawnObject.Begin.position);
            S1 = _distanceBetweenPoints;
            delta = (int)_obstacleSpawner.Obstacles[i].MinimumPossibleDistance / 2;

            maxNumberOfPoints = (S0 - delta) / S1 + 1;
            numberOfPoints = Random.Range(0, maxNumberOfPoints);

            for (int j = 0; j < numberOfPoints; j++)
            {
                typeOfPoint = Random.Range(0, 26);

                if (typeOfPoint == 0)
                {
                    SpawnPoint(snowball, _snowballPrefab, i, delta + j * S1);
                }
                else if (typeOfPoint == 1)
                {
                    SpawnPoint(heart, _heartPrefab, i, delta + j * S1);
                }
                else if (typeOfPoint > 1 && typeOfPoint < 16)
                {

                    SpawnPoint(candy, _candyPrefabs[0], i, delta + j * S1);
                }
                else
                    SpawnPoint(coin, _coinPrefab, i, delta + j * S1);

            }
        }
    }

    private void SpawnPoint(Point point, Point prefab, int stripNumber, int delta)
    {
        point = Instantiate(prefab);
        point.transform.position = _obstacleSpawner.Obstacles[stripNumber].transform.position - new Vector3(0, 0, delta);

        _spawnedPoints.Add(point);
    }
}
