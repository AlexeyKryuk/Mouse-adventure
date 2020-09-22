using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Wolf _wolf;
    [SerializeField] private Player _player;
    [SerializeField] private Shooter _shooter;

    [SerializeField] private Text _enemyText;
    [SerializeField] private Text _coinsText;
    [SerializeField] private Text _candyText;
    [SerializeField] private Slider _snowballs;
    [SerializeField] private Slider _healthBarSlider;
    [SerializeField] private Animator _healthBarAnimator;
    [SerializeField] private AudioClip[] _clips;
    [SerializeField] private GameObject _menu;
    [SerializeField] private GameObject _gameOver;

    public static int CountEnemy = 0;
    public static int CountCoins = 0;
    public static int CountCandy = 0;

    private void Start()
    {
        CountEnemy = 0;
        CountCoins = 0;
        CountCandy = 0;
        _snowballs.value = 10;
    }

    private void OnEnable()
    {
        _wolf.PickedUpPoint += OnPointPickedUp;
        _wolf.TookDamage += OnTookDamage;
        _player.EnemyKilled += OnEnemyKilled;
        _player.Died += OnPlayerDied;
        _shooter.SnowballThrown += OnCastSnowball;
    }

    private void OnDisable()
    {
        _wolf.PickedUpPoint -= OnPointPickedUp;
        _wolf.TookDamage -= OnTookDamage;
        _player.EnemyKilled -= OnEnemyKilled;
        _player.Died -= OnPlayerDied;
        _shooter.SnowballThrown -= OnCastSnowball;
    }

    private void OnPointPickedUp(Point point, int value)
    {
        if (point.TryGetComponent(out Candy candy))
        {
            OnPickUpCandy(value);
        }
        if (point.TryGetComponent(out Coin coin))
        {
            OnPickUpCoin(value);
        }
        if (point.TryGetComponent(out Heart heart))
        {
            OnPickUpHeart(value);
        }
        if (point.TryGetComponent(out SnowballPoint snowballPoint))
        {
            OnPickUpSnowball(value);
        }
    }

    private void OnTookDamage(int value, string obstacleName)
    {
        _healthBarSlider.value -= value;
    }

    private void OnPickUpCoin(int count)
    {
        CountCoins += count;
        _coinsText.text = CountCoins.ToString();

        GetComponent<AudioSource>().PlayOneShot(_clips[0]);
    }

    private void OnPickUpCandy(int count)
    {
        CountCandy += count;
        _candyText.text = CountCandy.ToString();
        GetComponent<AudioSource>().PlayOneShot(_clips[2]);
    }

    private void OnEnemyKilled()
    {
        CountEnemy++;
        _enemyText.text = CountEnemy.ToString();
    }

    private void OnPickUpSnowball(int count)
    {
        _snowballs.value += count;

        GetComponent<AudioSource>().PlayOneShot(_clips[1]);
    }

    private void OnCastSnowball()
    {
        _snowballs.value--;
    }

    private void OnPickUpHeart(int value)
    {
        _healthBarAnimator.SetTrigger("Heal");
        _healthBarSlider.value += value;

        GetComponent<AudioSource>().PlayOneShot(_clips[3]);
    }

    private void OnPlayerDied()
    {
        _gameOver.GetComponent<Animation>().Play();
    }

    public void PlayFirstScene()
    {
        SceneManager.LoadScene("FirstScene");
    }

    public void Play()
    {
        SceneManager.LoadScene("SecondScene");
    }

    public void PlayRunner()
    {
        SceneManager.LoadScene("RunnerScene");
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
