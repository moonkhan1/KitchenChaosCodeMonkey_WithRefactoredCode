using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private Transform _recipeTemplate;

    private void Awake()
    {
        _recipeTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSpawn += UpdateVisual;
        DeliveryManager.Instance.OnRecipeCompleted += UpdateVisual;
        UpdateVisual();
    }
    

    private void UpdateVisual()
    {
        foreach (Transform child in _container)     
        {
            if(child == _recipeTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach (RecipeSO recipeSo in DeliveryManager.Instance.GetWaitingRecipeSOList())
        {
            Transform recipeTransform = Instantiate(_recipeTemplate, _container);
            recipeTransform.gameObject.SetActive(true);
            recipeTransform.GetComponent<DeliverManagerSingleUI>().SetRecipeSO(recipeSo);
        }
    }
}
