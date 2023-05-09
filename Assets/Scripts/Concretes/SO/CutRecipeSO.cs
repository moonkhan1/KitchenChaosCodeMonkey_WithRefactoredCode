using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Kitchen", menuName = "Kitchen/Cut Recipe Object", order = 51)]
public class CutRecipeSO : ScriptableObject
{
    public KitchenObjecsSO input;
    public KitchenObjecsSO output;
    public int cuttingProgressMax;
}
