using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Kitchen", menuName = "Kitchen/Recipe", order = 51)]
public class RecipeSO : ScriptableObject
{
    public List<KitchenObjectsSO> kitchenObjectsSoList;
    public string recipeName;
}
