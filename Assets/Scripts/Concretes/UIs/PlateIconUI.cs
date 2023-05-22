using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlateIconUI : MonoBehaviour
{
    [SerializeField] private PlateKitchenObject _plateKitchenObject;
    [SerializeField] private Transform _iconTemplate;

    private void Awake()
    {
        _iconTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        _plateKitchenObject.OnIngredientAdded += PlateKitchenObjectOnOnIngredientAdded;
    }

    private void PlateKitchenObjectOnOnIngredientAdded(KitchenObjectsSO obj)
    {
        // foreach (Transform prevChild in transform)
        // {
        //     if(prevChild == _iconTemplate) continue;
        //     Destroy(prevChild.gameObject);
        // }
        // foreach (KitchenObjectsSO kitchenObjectsSo in _plateKitchenObject._kitchenObjectsSoOnPlateList)
        // {
        //     Transform icon = Instantiate(_iconTemplate, transform);
        //     icon.GetComponent<PlateSingleIconUI>().IconKitchenObject = kitchenObjectsSo.sprite;
        // }
        
        transform.Cast<Transform>().Where(prevChild => prevChild != _iconTemplate)
            .ToList()
            .ForEach(prevChild => Destroy(prevChild.gameObject));

        _plateKitchenObject._kitchenObjectsSoOnPlateList
            .ForEach(kitchenObjectsSo =>
            {
                Transform iconTemplate = Instantiate(_iconTemplate, transform);
                iconTemplate.gameObject.SetActive(true);
                iconTemplate.GetComponent<PlateSingleIconUI>().IconKitchenObject = kitchenObjectsSo.sprite;
            });

    }
}
