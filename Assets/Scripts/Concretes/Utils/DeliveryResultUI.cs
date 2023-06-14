using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryResultUI : MonoBehaviour
{
    private const string IS_POPUP = "Popup";
    [SerializeField] private Image _backGroundImage;
    [SerializeField] private Image _iconImage;
    [SerializeField] private TextMeshProUGUI _messageResultText;
    [SerializeField] private Color _succesColor;
    [SerializeField] private Color _failColor;
    [SerializeField] private Sprite _successSprite;
    [SerializeField] private Sprite _failSprite;

    private Animator _anim;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;
        Hide();
    }

    private void DeliveryManager_OnRecipeFailed()
    {
        Show();
        _anim.SetTrigger(IS_POPUP);
        _backGroundImage.color = _failColor;
        _iconImage.sprite = _failSprite;
        _messageResultText.text = "DELIVERY\nFAILED";
    }

    private void DeliveryManager_OnRecipeSuccess()
    {
        Show(); 
        _anim.SetTrigger(IS_POPUP);
        _backGroundImage.color = _succesColor;
        _iconImage.sprite = _successSprite;
        _messageResultText.text = "DELIVERY\nSUCCESS";
    }
    
    private void Show() => gameObject.SetActive(true);
    private void Hide() => gameObject.SetActive(false);
}
