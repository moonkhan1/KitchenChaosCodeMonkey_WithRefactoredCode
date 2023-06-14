using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveBurnFlashingBarUI : MonoBehaviour
{
    private const string IS_FLASHING = "IsFlashing";
    [SerializeField] private StoveCounter _stoveCounter;
    private Animator _anim;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    private void Start()
    {
        _stoveCounter.OnProgress += StoveCounter_OnProgress;
        _anim.SetBool(IS_FLASHING, false);
    }

    private void StoveCounter_OnProgress(float obj)
    {
        float burnShowProgressAmount = 0.5f;
        bool show = _stoveCounter.isFried() && obj >= burnShowProgressAmount;
        
        _anim.SetBool(IS_FLASHING, show);
    }
}
