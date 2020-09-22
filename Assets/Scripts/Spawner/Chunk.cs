using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
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

    public Transform Begin;
    public Transform End;

    private Obstacle[] _spawnedObstacles = new Obstacle[3]; //Препятствия

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
            // Размещаем дом слева
            newHouse = Instantiate(_housePrefabs[UnityEngine.Random.Range(0, _housePrefabs.Length)], _leftSide);
            newHouse.transform.position = _startPoint.position - newHouse.Begin.localPosition;

            // Размещаем дом справа
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
                maxRange = 18; // На средней полосе, при броске монеты не учитываем машины 
            else
                maxRange = 21; 

            int typeOfObstacle = UnityEngine.Random.Range(minRange, maxRange);  // Бросаем монету (выбираем какой тип препятсвтия выставим)

            if (typeOfObstacle >= 1 && typeOfObstacle <= 6)                     // 30 % шанс на Enemy
            {
                newObstacle = Instantiate(_enemyPrefabs[UnityEngine.Random.Range(0, _enemyPrefabs.Length)]);
                minRange = 7;
            }
            else if (typeOfObstacle >= 7 && typeOfObstacle <= 8)                // 10 % шанс на Ice
            {
                newObstacle = Instantiate(_icePrefabs[UnityEngine.Random.Range(0, _icePrefabs.Length)]);
            }
            else if (typeOfObstacle == 9)                                       // 5 % шанс на Composite
            {
                if (i == 0) // Только для первой полосы
                {
                    newObstacle = Instantiate(_compositePrefabs[UnityEngine.Random.Range(0, _compositePrefabs.Length)]);
                    newObstacle.transform.position = _anchors[i].position - newObstacle.AnchorPoint.localPosition; // Ставим препятсвтие в позицию

                    _spawnedObstacles[0] = newObstacle;
                    _spawnedObstacles[1] = newObstacle;
                    _spawnedObstacles[2] = newObstacle;
                    break;
                }
                else        // Иначе - ставим препятствие Ice
                    newObstacle = Instantiate(_icePrefabs[UnityEngine.Random.Range(0, _icePrefabs.Length)]);
            }
            else if (typeOfObstacle >= 10 && typeOfObstacle <= 18)              // 40 % шанс на Barrier
            {
                newObstacle = Instantiate(_barrierPrefabs[UnityEngine.Random.Range(0, _barrierPrefabs.Length)]);
            }
            else                                                                // 15 % шанс на Car
            {
                newObstacle = Instantiate(_carPrefabs[UnityEngine.Random.Range(0, _carPrefabs.Length)]);
            }

            newObstacle.transform.SetParent(_anchors[i]);   // Делаем новое препятсвтие потомком Якоря на полосе

            if (i != 0) // Первое препятсвтие выставляем без учёта его MPD, последующие - проверяем на mpd
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

            deltaZ = new Vector3(0f, 0f, UnityEngine.Random.Range(minDistance, maxDistance)); // Определяем смещение по оси Z относительно Якоря на полосе

            Vector3 newPosition = _anchors[i].position - newObstacle.AnchorPoint.localPosition - deltaZ; // Определяем новую позицию нового препятствия

            newObstacle.transform.position = newPosition; // Ставим препятсвтие в эту позицию

            _spawnedObstacles[i] = newObstacle; // Добавляем препятствие в список уже размещенных препятствий
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
