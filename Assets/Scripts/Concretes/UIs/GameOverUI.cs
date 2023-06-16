using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    
    [SerializeField] private TextMeshProUGUI _recipeDeliveredText;
    [SerializeField] private Button _playAgainButton;

    private void Awake()
    {
        _playAgainButton.onClick.AddListener(() =>
        {
            SceneManager.LoadSceneAsync("GameScene");
        });
}

    private void Start()
    {
        GameManager.Instance.OnStateChanged += InstanceOnStateChanged;
        Hide();
    }

    private void InstanceOnStateChanged()
    {
        if (GameManager.Instance.IsGameOver())
        {
            Show();
            _recipeDeliveredText.text = DeliveryManager.Instance.SuccessRecipeDelivered.ToString();
        }
        else
        {
            Hide();
        }
    }
    
    private void Show()
    {
        gameObject.gameObject.SetActive(true);
        _playAgainButton.Select();
    }
    private void Hide()
    {
        gameObject.gameObject.SetActive(false);
    }
}
