using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class DeliveryManager : MonoBehaviour
{
    public event System.Action OnRecipeSpawn;
    public event System.Action OnRecipeCompleted;
    public static DeliveryManager Instance { get; private set; }

    [SerializeField] private RecipeSOList _recipeListSO;
    private List<RecipeSO> waitingRecipeSOList;
    private float _recipeTimerBeforeSpawn;
    private const float _recipeTimerBeforeSpawnMax = 4f;
    private int _waitingMaxRecipeCount = 4;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        waitingRecipeSOList = new List<RecipeSO>();
    }

    private void Update()
    {
        _recipeTimerBeforeSpawn += Time.deltaTime;
        if (_recipeTimerBeforeSpawn >= _recipeTimerBeforeSpawnMax)
        {
            if (waitingRecipeSOList.Count >= _waitingMaxRecipeCount) return;
            RecipeSO _waitingRecipeSO = _recipeListSO.recipeSOList[Random.Range(0, _recipeListSO.recipeSOList.Count)];
            waitingRecipeSOList.Add(_waitingRecipeSO);
            OnRecipeSpawn?.Invoke();
            Debug.Log(_waitingRecipeSO.recipeName);
            _recipeTimerBeforeSpawn = 0f;
        }
        
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        foreach (RecipeSO waitingRecipeSO in waitingRecipeSOList)
        {
            if (waitingRecipeSO.kitchenObjectsSoList.Count == plateKitchenObject._kitchenObjectsSoOnPlateList.Count)
            {
                bool plateIngredientsMatchWithRecipe = waitingRecipeSO.kitchenObjectsSoList.All(recipeKitchenObjectSO =>
                    plateKitchenObject._kitchenObjectsSoOnPlateList.Contains(recipeKitchenObjectSO));

                if (plateIngredientsMatchWithRecipe)
                {
                    waitingRecipeSOList.Remove(waitingRecipeSO);
                    OnRecipeCompleted?.Invoke();
                    Debug.Log("Player delivered correct recipe: " + waitingRecipeSO);
                    return;
                }
            }
        }
        Debug.Log("Player delivered incorrect recipe");
        
    }

    public List<RecipeSO> GetWaitingRecipeSOList()
    {
        return waitingRecipeSOList;
    }
    
}


