using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class PlateKitchenObject : KitchenObjectController
{
    [SerializeField] private List<KitchenObjectsSO> _validKitchenObjecsSoOnPlateArray; // Only sliced form or valid ingredients.
    public List<KitchenObjectsSO> _kitchenObjectsSoOnPlateList{ get; private set; }
    public event Action<KitchenObjectsSO> OnIngredientAdded; 

    private void Awake()
    {
        _kitchenObjectsSoOnPlateList = new List<KitchenObjectsSO>();
    }

    public bool TryAddIngredient(KitchenObjectsSO kitchenObjectSo)
    {
        if (!_validKitchenObjecsSoOnPlateArray.Contains(kitchenObjectSo)) return false;
        if (_kitchenObjectsSoOnPlateList.Contains(kitchenObjectSo)) return false;
        _kitchenObjectsSoOnPlateList.Add(kitchenObjectSo);
        OnIngredientAdded?.Invoke(kitchenObjectSo);
        return true;
    }
}

