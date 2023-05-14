using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Kitchen", menuName = "Kitchen/Frying Recipe Object", order = 51)]
public class FryingRecipeSO : ScriptableObject
{
    public KitchenObjecsSO input;
    public KitchenObjecsSO output;
    public float fryingTimerMax;
}
