using System;
using System.Collections;
using System.Collections.Generic;
using Kitchen.Abstract.Controller;
using UnityEngine;
using UnityEngine.Serialization;

public class ClearCounter : BaseCounter
{
    [FormerlySerializedAs("_kitchenObjecsSo")] [SerializeField] private KitchenObjectsSO kitchenObjectsSo;
    
    public override void Interact(PlayerController player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                player.KitchenObject.KitchenObjectsParent = this;
            }
        }
        else
        {
            if (!player.HasKitchenObject())
            {
                KitchenObject.KitchenObjectsParent = player;
            }
            else
            {
                if (player.KitchenObject.TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    if (plateKitchenObject.TryAddIngredient(KitchenObject.GetKitchenObjectSO()))
                    {
                        KitchenObject.DestroySelf();
                    }
                }
                else
                {
                    // Not plate but other kitchen object on player
                    if (KitchenObject.TryGetPlate(out plateKitchenObject))
                    {
                        // There is Plate on Counter
                        if (plateKitchenObject.TryAddIngredient(player.KitchenObject.GetKitchenObjectSO()))
                        {
                            player.KitchenObject.DestroySelf();
                        }
                    }
                }
            }
        }
    }


}
