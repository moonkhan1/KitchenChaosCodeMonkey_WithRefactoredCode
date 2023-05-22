using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Kitchen", menuName = "Kitchen/Recipe List", order = 51)]
public class RecipeSOList : ScriptableObject
{
    public List<RecipeSO> recipeSOList;
}
