using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : BaseCounter
{
    public static DeliveryCounter Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public override void Interact(PlayerController player)
    {
        if(!player.HasKitchenObject()) return;
        if (player.KitchenObject.TryGetPlate(out PlateKitchenObject plateKitchenObject))
        {
            DeliveryManager.Instance.DeliverRecipe(plateKitchenObject);
            player.KitchenObject.DestroySelf();
        }
    }
}
