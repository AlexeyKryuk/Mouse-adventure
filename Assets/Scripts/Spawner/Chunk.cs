using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : SpawnObject
{
    [SerializeField] private bool _isFirstChunk;

    [SerializeField] private House[] _housePrefabs;

    [SerializeField] private Enemy[] _enemyPrefabs;
    [SerializeField] private Ice[] _icePrefabs;
    [SerializeField] private Car[] _carPrefabs;
    [SerializeField] private Barrier[] _barrierPrefabs;
    [SerializeField] private Composite[] _compositePrefabs;

    [SerializeField] private Coin _coinPrefab;
    [SerializeField] private SnowballPoint _snowballPrefab;
    [SerializeField] private Heart _heartPrefab;
    [SerializeField] private Candy[] _candyPrefabs;

    [SerializeField] private Transform _leftSide;
    [SerializeField] private Transform _rightSide;

    [SerializeField] private Transform[] _anchors;

    [SerializeField] private int _numberOfHouse;
    [SerializeField] private int _numberOfStripes = 3;


    private Obstacle[] _spawnedObstacles = new Obstacle[3];

    private int _distanceBetweenCoins = 3;

    private void Start()
    {
        GenerateHouses();

        if (!_isFirstChunk)
            GenerateObstacles();
    }

    private void GenerateHouses()
    {
        House newHouse = null;
        Transform _startPoint = Begin;

        for (int i = 0; i < _numberOfHouse; i++)
        {
            newHouse = Instantiate(_housePrefabs[UnityEngine.Random.Range(0, _housePrefabs.Length)], _leftSide);
            Debug.Log(Begin.position);
            newHouse.transform.position = _startPoint.position - newHouse.Begin.localPosition;

            newHouse = Instantiate(_housePrefabs[UnityEngine.Random.Range(0, _housePrefabs.Length)], _rightSide);
            newHouse.transform.Rotate(Vector3.up, 180f);
            newHouse.transform.position = _startPoint.position + newHouse.End.localPosition;

            _startPoint = newHouse.Begin;
        }
    }

    private void GenerateObstacles()
    {
        Obstacle newObstacle = null;
        Vector3 deltaZ = Vector3.zero;

        int minDistance = 0;
        int maxDistance = 25;

        int maxRange;
        int minRange = 1;

        for (int i = 0; i < _numberOfStripes; i++)
        {
            if (i == 1)
                maxRange = 18;
            else
                maxRange = 21; 

            int typeOfObstacle = UnityEngine.Random.Range(minRange, maxRange);

            if (typeOfObstacle >= 1 && typeOfObstacle <= 6)
            {
                newObstacle = Instantiate(_enemyPrefabs[UnityEngine.Random.Range(0, _enemyPrefabs.Length)]);
                minRange = 7;
            }
            else if (typeOfObstacle >= 7 && typeOfObstacle <= 8)
            {
                newObstacle = Instantiate(_icePrefabs[UnityEngine.Random.Range(0, _icePrefabs.Length)]);
            }
            else if (typeOfObstacle == 9)
            {
                if (i == 0)
                {
                    newObstacle = Instantiate(_compositePrefabs[UnityEngine.Random.Range(0, _compositePrefabs.Length)]);
                    newObstacle.transform.position = _anchors[i].position - newObstacle.AnchorPoint.localPosition;

                    _spawnedObstacles[0] = newObstacle;
                    _spawnedObstacles[1] = newObstacle;
                    _spawnedObstacles[2] = newObstacle;
                    break;
                }
                else
                    newObstacle = Instantiate(_icePrefabs[UnityEngine.Random.Range(0, _icePrefabs.Length)]);
            }
            else if (typeOfObstacle >= 10 && typeOfObstacle <= 18)
            {
                newObstacle = Instantiate(_barrierPrefabs[UnityEngine.Random.Range(0, _barrierPrefabs.Length)]);
            }
            else                                                                
            {
                newObstacle = Instantiate(_carPrefabs[UnityEngine.Random.Range(0, _carPrefabs.Length)]);
            }

            newObstacle.transform.SetParent(_anchors[i]);

            if (i != 0) 
            {
                if (deltaZ.z < newObstacle.MinimumPossibleDistance)
                {
                    minDistance = (int)deltaZ.z + newObstacle.MinimumPossibleDistance;
                    maxDistance = 25;
                }
                else
                {
                    maxDistance = (int)deltaZ.z - newObstacle.MinimumPossibleDistance;
                    minDistance = 0;
                }
            }

            deltaZ = new Vector3(0f, 0f, UnityEngine.Random.Range(minDistance, maxDistance)); 

            Vector3 newPosition = _anchors[i].position - newObstacle.AnchorPoint.localPosition - deltaZ; 

            newObstacle.transform.position = newPosition; 

            _spawnedObstacles[i] = newObstacle; 
        }

        GeneratePoints();
    }

    private void GeneratePoints()
    {
        int S0, S1, delta, maxNumberOfCoins, numberOfCoins, typeOfCoin;
        Coin coin = null;
        SnowballPoint snowball = null;
        Heart heart = null;
        Candy candy = null;

        for (int i = 0; i < _numberOfStripes; i++)
        {
            if (_spawnedObstacles[i].GetType().Name != "Enemy")
            {
                if (UnityEngine.Random.Range(0, 2) == 0)
                {
                    S0 = (int)Vector3.Distance(_spawnedObstacles[i].AnchorPoint.position, Begin.position);
                    S1 = _distanceBetweenCoins;
                    delta = _spawnedObstacles[i].MinimumPossibleDistance / 2;

                    maxNumberOfCoins = (S0 - delta) / S1 + 1;
                    numberOfCoins = UnityEngine.Random.Range(0, maxNumberOfCoins);

                    for (int j = 0; j < numberOfCoins; j++)
                    {
                        typeOfCoin = UnityEngine.Random.Range(0, 26);

                        if (typeOfCoin == 0)
                        {
                            SpawnPoint(snowball, _snowballPrefab, i, delta + j * S1);
                        }
                        else if (typeOfCoin == 1)
                        {
                            SpawnPoint(heart, _heartPrefab, i, delta + j * S1);
                        }
                        else if (typeOfCoin > 1 && typeOfCoin < 16)
                        {

                            SpawnPoint(candy, _candyPrefabs[0], i, delta + j * S1);
                        }
                        else
                            SpawnPoint(coin, _coinPrefab, i, delta + j * S1);
                       
                    }
                }
            }
        }
    }

    private void SpawnPoint(Point point, Point prefab, int stripNumber, int delta)
    {
        point = Instantiate(prefab);
        point.transform.SetParent(_spawnedObstacles[stripNumber].AnchorPoint);
        point.transform.position = _spawnedObstacles[stripNumber].AnchorPoint.position - new Vector3(0, 0, delta);
    }
}
