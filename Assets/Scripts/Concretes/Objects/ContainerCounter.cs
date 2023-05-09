using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    [SerializeField] private KitchenObjecsSO _kitchenObjecsSo;
    public override event Action<string> OnAnimationPlay;
    private const string OPEN_CLOSE_ANIMATION = "OpenClose";
    public override void Interact(PlayerController playerController)
    {
        // Do not instantiate more than one time
        if (!playerController.HasKitchenObject())
        {
            KitchenObjectController.SpawnKitchenObject(_kitchenObjecsSo, playerController);
            OnAnimationPlay?.Invoke(OPEN_CLOSE_ANIMATION);
        }
    }
    
}
