using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _recipeDeliveredText;

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
    }
    private void Hide()
    {
        gameObject.gameObject.SetActive(false);
    }
}
