using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SceneController : MonoBehaviour 
{
    [SerializeField] private GameObject StartMenu;
    [SerializeField] private GameObject escapeMenu;

    private bool _isGameStarted = false;
    private bool _isActiveMenu = false;

    public event UnityAction<bool> GamePaused;

    private void Awake()
    {
        Time.timeScale = 0;
        GamePaused?.Invoke(true);
    }

    private void Update()
    {
        if (!_isGameStarted)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
            {
                _isGameStarted = true;
                
                Time.timeScale = 1;
                StartMenu.SetActive(false);
                GamePaused?.Invoke(false);
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
                    GamePaused?.Invoke(true);
                    escapeMenu.SetActive(true);

                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }
                else
                {
                    Time.timeScale = 1;
                    GamePaused?.Invoke(false);
                    escapeMenu.SetActive(false);

                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                }
            }
        }
    }
}
