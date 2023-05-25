using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ContainerCounter : BaseCounter
{
    [FormerlySerializedAs("_kitchenObjecsSo")] [SerializeField] private KitchenObjectsSO kitchenObjectsSo;
    public override event Action<string> OnAnimationPlay;
    public static event Action<ContainerCounter> OnContainerTake; 
    private const string OPEN_CLOSE_ANIMATION = "OpenClose";
    public override void Interact(PlayerController playerController)
    {
        // Do not instantiate more than one time
        if (!playerController.HasKitchenObject())
        {
            KitchenObjectController.SpawnKitchenObject(kitchenObjectsSo, playerController);
            OnContainerTake?.Invoke(this);
            OnAnimationPlay?.Invoke(OPEN_CLOSE_ANIMATION);
        }
    }
    
}
