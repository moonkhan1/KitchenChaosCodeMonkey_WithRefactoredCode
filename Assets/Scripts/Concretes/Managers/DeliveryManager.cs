using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using UnityEngine;

public class DeliveryManager : SingletonBase<DeliveryManager>
{
    public event System.Action OnRecipeSpawn;
    public event System.Action OnRecipeCompleted;
    public event System.Action OnRecipeSuccess;
    public event System.Action OnRecipeFailed;

    [SerializeField] private RecipeSOList _recipeListSO;
    private List<RecipeSO> waitingRecipeSOList;
    private float _recipeTimerBeforeSpawn = 4f;
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
        if (!IsServer) return;
        _recipeTimerBeforeSpawn -= Time.deltaTime;
        if (_recipeTimerBeforeSpawn <= 0f)
        {
            _recipeTimerBeforeSpawn = _recipeTimerBeforeSpawnMax;
            if (GameManager.Instance.IsGamePlaying() && waitingRecipeSOList.Count < _waitingMaxRecipeCount)
            {
                int _waitingRecipeSOIndex = Random.Range(0, _recipeListSO.recipeSOList.Count);
                SpawnNewWaitingRecipeClientRpc(_waitingRecipeSOIndex);

            }

        }
        
    }
    [ClientRpc]
    private void SpawnNewWaitingRecipeClientRpc(int _waitingRecipeSOListIndex)
    {
        RecipeSO _waitingRecipeSO = _recipeListSO.recipeSOList[_waitingRecipeSOListIndex];
        waitingRecipeSOList.Add(_waitingRecipeSO);
        OnRecipeSpawn?.Invoke();
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        for (int i = 0; i < waitingRecipeSOList.Count; i++)
        {
            var waitingRecipeSO = waitingRecipeSOList[i];
            if (waitingRecipeSO.kitchenObjectsSoList.Count == plateKitchenObject._kitchenObjectsSoOnPlateList.Count)
            {
                bool plateIngredientsMatchWithRecipe = waitingRecipeSO.kitchenObjectsSoList.All(recipeKitchenObjectSO =>
                    plateKitchenObject._kitchenObjectsSoOnPlateList.Contains(recipeKitchenObjectSO));

                if (plateIngredientsMatchWithRecipe)
                {
                    Debug.Log("Player delivered correct recipe: " + waitingRecipeSO);
                    DeliverCorrectRecipeServerRpc(i);
                    return;
                }
            }
        }

        Debug.Log("Player delivered incorrect recipe");
        DeliverIncorrectRecipeServerRpc();

    }
    
    [ServerRpc(RequireOwnership = false)]
    private void DeliverIncorrectRecipeServerRpc()
    {
        DeliverIncorrectRecipeClientRpc();
    }

    [ClientRpc]
    private void DeliverIncorrectRecipeClientRpc()
    {
        OnRecipeFailed?.Invoke();
    }
    
    [ServerRpc(RequireOwnership = false)]
    private void DeliverCorrectRecipeServerRpc(int _waitingRecipeSOListIndex)
    {
        DeliverCorrectRecipeClientRpc(_waitingRecipeSOListIndex);
    }

    [ClientRpc]
    private void DeliverCorrectRecipeClientRpc(int _waitingRecipeSOListIndex)
    {
        waitingRecipeSOList.RemoveAt(_waitingRecipeSOListIndex);
        _successRecipeDelivered++;
        OnRecipeCompleted?.Invoke();
        OnRecipeSuccess?.Invoke(); 
    }
    
    public List<RecipeSO> GetWaitingRecipeSOList()
    {
        return waitingRecipeSOList;
    }
    
}


