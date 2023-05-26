using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    private PlayerController _playerController;
    private float _footStepsTimer;
    private readonly float _footStepsTimerMax = 0.1f;

    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        _footStepsTimer += Time.deltaTime;
        if (_footStepsTimer >= _footStepsTimerMax)
        {
            if (_playerController.Direction.magnitude > 0f)
            {
                float volume = 1f;
                SoundManager.Instance.PlayFootstepsSound(_playerController.transform.position, volume);
                _footStepsTimer = 0f;
            }

        }
            
    }
}
