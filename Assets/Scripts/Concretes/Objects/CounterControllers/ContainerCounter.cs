using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ContainerCounter : BaseCounter
{
    [FormerlySerializedAs("_kitchenObjecsSo")] [SerializeField] private KitchenObjectsSO kitchenObjectsSo;
    public override event Action<string> OnAnimationPlay;
    private const string OPEN_CLOSE_ANIMATION = "OpenClose";
    public override void Interact(PlayerController playerController)
    {
        // Do not instantiate more than one time
        if (!playerController.HasKitchenObject())
        {
            KitchenObjectController.SpawnKitchenObject(kitchenObjectsSo, playerController);
            OnAnimationPlay?.Invoke(OPEN_CLOSE_ANIMATION);
        }
    }
    
}
