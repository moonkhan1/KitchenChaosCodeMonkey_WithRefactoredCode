using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectsParent
{
    [SerializeField] private Transform _counterTopPoint;
    
    public virtual event System.Action<string> OnAnimationPlay;
    //public virtual event EventHandler<EventArgs> OnParticlePlay;  
    public virtual event System.Action<bool> OnParticlePlay;

    public KitchenObjectController KitchenObject { get; set; }

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
