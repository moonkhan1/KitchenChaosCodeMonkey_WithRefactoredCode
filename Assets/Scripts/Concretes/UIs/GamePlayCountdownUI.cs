using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GamePlayCountdownUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textMeshProUGUI;

    private void Start()
    {
        GameManager.Instance.OnStateChanged += InstanceOnStateChanged;
        Hide();
    }

    private void Update()
    {
        _textMeshProUGUI.text = Mathf.Ceil(GameManager.Instance.GetCountdownNumber()).ToString();
    }

    private void InstanceOnStateChanged()
    {
        if (GameManager.Instance.IsCountdownStartSetActive())
        {
            Show();
        }
        else
        {
            Hide();
        }
    }
    
    private void Show()
    {
        _textMeshProUGUI.gameObject.SetActive(true);
    }
    private void Hide()
    {
        _textMeshProUGUI.gameObject.SetActive(false);
    }
    
}
