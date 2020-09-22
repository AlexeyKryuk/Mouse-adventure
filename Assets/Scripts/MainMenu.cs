using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Text _enemyText;
    [SerializeField] private Text _coinsText;
    [SerializeField] private Text _candyText;

    public GameObject LevelsWindow;
    public GameObject Menu;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        _coinsText.text = UIController.CountCoins.ToString();
        _enemyText.text = UIController.CountEnemy.ToString();
        _candyText.text = UIController.CountCandy.ToString();
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

    public void Levels(bool value)
    {
        LevelsWindow.SetActive(value);
        Menu.SetActive(!value);
    }
}
