using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GamePlayCountdownUI : MonoBehaviour
{
    private const string NUMBER_POPUP = "CountdownPopup";
    [SerializeField] private TextMeshProUGUI _textMeshProUGUI;
    private Animator _anim;
    private int previousCountdownNumber;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    private void Start()
    {
        GameManager.Instance.OnStateChanged += InstanceOnStateChanged;
        Hide();
    }

    private void Update()
    {
        int countdownNumber = Mathf.CeilToInt(GameManager.Instance.GetCountdownNumber());
        _textMeshProUGUI.text = countdownNumber.ToString();
        if (previousCountdownNumber != countdownNumber)
        {
            previousCountdownNumber = countdownNumber;
            _anim.SetTrigger(NUMBER_POPUP);
            SoundManager.Instance.PlayCountdownSound();
        }
        
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
