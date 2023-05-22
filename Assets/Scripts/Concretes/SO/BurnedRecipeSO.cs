using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Kitchen", menuName = "Kitchen/Burning Recipe Object", order = 51)]
public class BurnedRecipeSO : ScriptableObject
{
    public KitchenObjectsSO input;
    public KitchenObjectsSO output;
    public float burningTimerMax;
}
