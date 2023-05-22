using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{
    [Serializable]
    struct KitchenObjectSO_GameObjects
    {
        public KitchenObjectsSO kitchenObjectSo;
        public GameObject kitchenObjectGameObject;
    }
    
    [SerializeField] private PlateKitchenObject _plateKitchenObject;
    [SerializeField] private List<KitchenObjectSO_GameObjects> _kitchenObjectSoGameObjectsList;

    private void Start()
    {
        _plateKitchenObject.OnIngredientAdded += IngredientAddedOnPlate;
        _kitchenObjectSoGameObjectsList.ForEach(u => u.kitchenObjectGameObject.SetActive(false));
    }

    private void IngredientAddedOnPlate(KitchenObjectsSO kitchenObjectSo)
    {
        _kitchenObjectSoGameObjectsList.ForEach(u =>
        {
            if (u.kitchenObjectSo == kitchenObjectSo)
                u.kitchenObjectGameObject.SetActive(true);

        });
    }
}
