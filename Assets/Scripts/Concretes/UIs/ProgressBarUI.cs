using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private GameObject _hasProgressGameObject;
    [SerializeField] private Image _barImage;

    private IHasProgress _hasProgress;

    private void Start()
    {
        _hasProgress = _hasProgressGameObject.GetComponent<IHasProgress>();
        if(_hasProgress == null) Debug.LogError("Has no IHasProgress component");
        if (_hasProgress != null) _hasProgress.OnProgress += CounterHasOnProgress;
        _barImage.fillAmount = 0f;
        Hide();
    }

    private void CounterHasOnProgress(float progress)
    {
        _barImage.fillAmount = progress;
        if (progress is 0f or 1f)
        {
            Hide();
        }
        else
        {
            Show();
        }
    }

    private void Show()
    { 
        gameObject.SetActive(true); 
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
    
}
