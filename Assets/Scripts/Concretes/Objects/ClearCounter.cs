using System;
using System.Collections;
using System.Collections.Generic;
using Kitchen.Abstract.Controller;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField] private KitchenObjecsSO _kitchenObjecsSo;
    
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
        }
    }


}
