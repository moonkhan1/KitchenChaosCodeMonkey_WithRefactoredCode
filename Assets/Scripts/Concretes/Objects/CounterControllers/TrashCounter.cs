using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : BaseCounter
{
    public static event System.Action<TrashCounter> OnThrow; 
    public override void Interact(PlayerController player)
    {
        if (player.HasKitchenObject() && player.KitchenObject.GetKitchenObjectSO())
        {
            OnThrow?.Invoke(this);
            player.KitchenObject.DestroySelf();
        }
    }
}
