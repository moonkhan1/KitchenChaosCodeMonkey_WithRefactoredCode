using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DeliveryManager : MonoBehaviour
{
    [SerializeField] private RecipeSOList _recipeListSO;
    private List<RecipeSO> waitingRecipeSOList;

    private float _recipeTimerBeforeSpawn;
    private const float _recipeTimerBeforeSpawnMax = 4f;
    private int _waitingMaxRecipeCount = 4;

    private void Awake()
    {
        waitingRecipeSOList = new List<RecipeSO>();
    }

    private void Update()
    {
        _recipeTimerBeforeSpawn += Time.deltaTime;
        if (_recipeTimerBeforeSpawn >= _recipeTimerBeforeSpawnMax)
        {
            if (waitingRecipeSOList.Count >= _waitingMaxRecipeCount) return;
            RecipeSO _waitingRecipeSO = _recipeListSO.recipeSOList[Random.Range(0, _recipeListSO.recipeSOList.Count)];
            Debug.Log(_waitingRecipeSO.recipeName);
            waitingRecipeSOList.Add(_waitingRecipeSO);
            _recipeTimerBeforeSpawn = 0f;
        }
        
    }
}
