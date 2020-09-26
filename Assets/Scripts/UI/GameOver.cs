using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Animation _animation;
    [SerializeField] private Button _restart;
    [SerializeField] private Button _exit;

    private void OnEnable()
    {
        _player.Died += OnPlayerDied;
        _restart.onClick.AddListener(OnClickRestart);
        _exit.onClick.AddListener(OnClickExit);
    }

    private void OnDisable()
    {
        _player.Died -= OnPlayerDied;
        _restart.onClick.RemoveListener(OnClickRestart);
        _exit.onClick.RemoveListener(OnClickExit);
    }

    private void OnPlayerDied()
    {
        _animation.Play();

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void OnClickRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnClickExit()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
