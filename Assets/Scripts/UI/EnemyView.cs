using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyView : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Text _countOfEnemyText;

    private int _countOfEnemy;

    private void OnEnable()
    {
        _player.EnemyKilled += OnEnemyKilled;
    }

    private void OnDisable()
    {
        _player.EnemyKilled -= OnEnemyKilled;
    }

    private void OnEnemyKilled()
    {
        _countOfEnemy++;
        _countOfEnemyText.text = _countOfEnemy.ToString();
    }
}
