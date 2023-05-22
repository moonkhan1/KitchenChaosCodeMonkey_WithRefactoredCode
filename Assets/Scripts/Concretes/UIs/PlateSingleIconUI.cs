using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlateSingleIconUI : MonoBehaviour
{
    [SerializeField] private Image _iconKitchenObject;
    public Sprite IconKitchenObject
    {
        set => _iconKitchenObject.sprite = value;
    }
}
