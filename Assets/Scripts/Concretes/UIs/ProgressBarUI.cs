using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private CuttingCounter _cuttingCounter;
    [SerializeField] private Image _barImage;

    private void Start()
    {
        _cuttingCounter.OnProgress += CuttingCounterOnOnProgress;
        _barImage.fillAmount = 0f;
        Hide();
    }

    private void CuttingCounterOnOnProgress(float progress)
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
