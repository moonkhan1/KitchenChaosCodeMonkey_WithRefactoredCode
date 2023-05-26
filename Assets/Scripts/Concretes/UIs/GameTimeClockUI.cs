using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimeClockUI : MonoBehaviour
{
    [SerializeField] private Image _clockTimeImage;

    private void Update()
    {
        _clockTimeImage.fillAmount = GameManager.Instance.GamePlayTimer;
    }
}
