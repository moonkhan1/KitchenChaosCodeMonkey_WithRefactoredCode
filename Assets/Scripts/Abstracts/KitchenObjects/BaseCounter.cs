using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectsParent
{
    [SerializeField] private Transform _counterTopPoint;
    private KitchenObjectController _kitchenObject;

    public virtual event Action<string> OnAnimationPlay;
    //public virtual event EventHandler<EventArgs> OnParticlePlay;  
    public virtual event Action<bool> OnParticlePlay;

    public static event Action<BaseCounter> OnKitchenObjectPlacedHere;
    public KitchenObjectController KitchenObject
    {
        get => _kitchenObject;
        set
        {
            _kitchenObject = value;
            if (_kitchenObject != null)
            {
                OnKitchenObjectPlacedHere?.Invoke(this);
            }
        }
    }

    public virtual void Interact(PlayerController player)
    {
        Debug.LogError("BaseCounter.Interact()");
    }
    public virtual void InteractAlternate(PlayerController player)
    {
        Debug.LogError("BaseCounter.InteractAlternate()");
    }
    
    
    public Transform GetKitchenObjectControllerCounterTransform()
    {
        return _counterTopPoint;
    }

    public void ClearKitchenObject()
    {
        KitchenObject = null;
    }
    public bool HasKitchenObject()
    {
        return KitchenObject != null;
    }

}
