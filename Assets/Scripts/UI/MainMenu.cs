using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Text _enemyText;
    [SerializeField] private Text _coinsText;
    [SerializeField] private Text _candyText;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        _coinsText.text = _player.CountOfCoins.ToString();
        _enemyText.text = _player.CountOfKilledEnemies.ToString();
        _candyText.text = _player.CountOfCandies.ToString();
    }

    public void Play()
    {
        SceneManager.LoadScene("FirstScene");
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void PlayRunner()
    {
        SceneManager.LoadScene("RunnerScene");
    }
}
