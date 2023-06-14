using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class DeliveryManager : SingletonBase<DeliveryManager>
{
    public event System.Action OnRecipeSpawn;
    public event System.Action OnRecipeCompleted;
    public event System.Action OnRecipeSuccess;
    public event System.Action OnRecipeFailed;

    [SerializeField] private RecipeSOList _recipeListSO;
    private List<RecipeSO> waitingRecipeSOList;
    private float _recipeTimerBeforeSpawn;
    private const float _recipeTimerBeforeSpawnMax = 4f;
    private int _waitingMaxRecipeCount = 4;
    private int _successRecipeDelivered;
    public int SuccessRecipeDelivered => _successRecipeDelivered;

    private void Awake()
    {
        MakeSingleton(this);
        waitingRecipeSOList = new List<RecipeSO>();
    }

    private void Update()
    {
        _recipeTimerBeforeSpawn += Time.deltaTime;
        if (_recipeTimerBeforeSpawn >= _recipeTimerBeforeSpawnMax)
        {
            if (GameManager.Instance.IsGamePlaying() && waitingRecipeSOList.Count < _waitingMaxRecipeCount)
            {
                RecipeSO _waitingRecipeSO = _recipeListSO.recipeSOList[Random.Range(0, _recipeListSO.recipeSOList.Count)];
                waitingRecipeSOList.Add(_waitingRecipeSO);
                OnRecipeSpawn?.Invoke();
                Debug.Log(_waitingRecipeSO.recipeName);
                _recipeTimerBeforeSpawn = 0f;
            }

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
                    _successRecipeDelivered++;
                    OnRecipeCompleted?.Invoke();
                    OnRecipeSuccess?.Invoke();
                    Debug.Log("Player delivered correct recipe: " + waitingRecipeSO);
                    return;
                }
            }
        }
        Debug.Log("Player delivered incorrect recipe");
        OnRecipeFailed?.Invoke();
        
    }

    public List<RecipeSO> GetWaitingRecipeSOList()
    {
        return waitingRecipeSOList;
    }
    
}


