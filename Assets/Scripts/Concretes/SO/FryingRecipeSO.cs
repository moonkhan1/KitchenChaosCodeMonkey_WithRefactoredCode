using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Kitchen", menuName = "Kitchen/Frying Recipe Object", order = 51)]
public class FryingRecipeSO : ScriptableObject
{
    public KitchenObjectsSO input;
    public KitchenObjectsSO output;
    public float fryingTimerMax;
}
