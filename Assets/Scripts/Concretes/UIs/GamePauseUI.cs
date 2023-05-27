using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{
    [SerializeField] private Button _returnButton; 
    [SerializeField] private Button _quitButton;

    private void Awake()
    {
        _returnButton.onClick.AddListener(() =>
        {
            GameManager.Instance.TogglePauseGame();
        });
        _quitButton.onClick.AddListener(() =>
        {
            SceneLoader.LoadSceneAsyncCustom(SceneLoader.Scene.MenuScene, 3);
        });
    }

    private void Start()
    {
        GameManager.Instance.OnGamePaused += GameManager_OnGamePaused;
        GameManager.Instance.OnGameUnPaused += GameManager_OnGameUnPaused;
        Hide();
    }

    private void GameManager_OnGameUnPaused()
    {
        Hide();
    }

    private void GameManager_OnGamePaused()
    {
        Show();
    }

    private void Show() => gameObject.SetActive(true);
    private void Hide() => gameObject.SetActive(false);
}
