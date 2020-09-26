using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EscapeMenu : MonoBehaviour 
{
    [SerializeField] private Button RestartButton;
    [SerializeField] private Button ExitButton;
    [SerializeField] private GameObject _controlTips;

    private bool _isGameStarted = false;
    private bool _isActiveMenu = false;

    private void Awake()
    {
        Time.timeScale = 0;
    }

    private void OnEnable()
    {
        RestartButton.onClick.AddListener(OnRestartButtonClick);
        ExitButton.onClick.AddListener(OnExitButtonClick);
    }

    private void OnDisable()
    {
        RestartButton.onClick.RemoveListener(OnRestartButtonClick);
        ExitButton.onClick.RemoveListener(OnExitButtonClick);
    }

    private void Update()
    {
        if (!_isGameStarted)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
            {
                _isGameStarted = true;
                _controlTips.SetActive(false);


                Time.timeScale = 1;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                _isActiveMenu = !_isActiveMenu;

                if (_isActiveMenu)
                {
                    Time.timeScale = 0;
                    ExitButton.gameObject.SetActive(true);
                    RestartButton.gameObject.SetActive(true);

                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }
                else
                {
                    Time.timeScale = 1;
                    ExitButton.gameObject.SetActive(false);
                    RestartButton.gameObject.SetActive(false);

                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                }
            }
        }
    }

    private void OnRestartButtonClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnExitButtonClick()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
