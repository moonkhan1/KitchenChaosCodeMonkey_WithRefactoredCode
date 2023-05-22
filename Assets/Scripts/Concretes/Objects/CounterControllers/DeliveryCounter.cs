using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : BaseCounter
{
    public override void Interact(PlayerController player)
    {
        if(!player.HasKitchenObject()) return;
        if (player.KitchenObject.TryGetPlate(out PlateKitchenObject plateKitchenObject))
        {
            player.KitchenObject.DestroySelf();
        }
    }
}
