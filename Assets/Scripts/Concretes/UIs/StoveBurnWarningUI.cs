using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveBurnWarningUI : MonoBehaviour
{
    [SerializeField] private StoveCounter _stoveCounter;

    private void Start()
    {
        _stoveCounter.OnProgress += StoveCounter_OnProgress;
        Hide();
    }

    private void StoveCounter_OnProgress(float obj)
    {
        float burnShowProgressAmount = 0.5f;
        bool show = _stoveCounter.isFried() && obj >= burnShowProgressAmount;
        
        if(show) Show();
        else Hide();
    }

    private void Show() => gameObject.SetActive(true);
    private void Hide() => gameObject.SetActive(false);
}
