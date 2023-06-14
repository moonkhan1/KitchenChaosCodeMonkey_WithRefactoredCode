using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterSound : MonoBehaviour
{
    [SerializeField] private StoveCounter _stoveCounter;

    private AudioSource _audioSource;
    private float _warningSoundTimer;
    private bool playWarningSound;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        _stoveCounter.OnStateChanged += StoveCounterOnStateChanged;
        _stoveCounter.OnProgress += StoveCounter_OnProgress;
    }

    private void Update()
    {
        if(!playWarningSound) return;
        _warningSoundTimer -= Time.deltaTime;
        if (_warningSoundTimer <= 0f)
        {
            float warningSoundTimerMax = 0.2f;
            _warningSoundTimer = warningSoundTimerMax;
            SoundManager.Instance.PlayWarningSound(_stoveCounter.transform.position);
        }
    }

    private void StoveCounter_OnProgress(float obj)
    {
        float burnShowProgressAmount = 0.5f;
        playWarningSound = _stoveCounter.isFried() && obj >= burnShowProgressAmount;
    }

    private void StoveCounterOnStateChanged(StoveCounter.State state)
    {
        bool playSound = state is StoveCounter.State.Frying or StoveCounter.State.Fried;
        if (playSound)
        {
            _audioSource.Play();
        }
        else
        {
            _audioSource.Pause();
        }
    }
}
