using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObjectController : MonoBehaviour
{
    [SerializeField] private KitchenObjecsSO _kitchenObjecsSo;

    private IKitchenObjectsParent _kitchenObjects;
    public IKitchenObjectsParent KitchenObjectsParent
    {
        get => _kitchenObjects;
        set
        {
            if (_kitchenObjects != null)
            {
                _kitchenObjects.ClearKitchenObject(); // Clear old one
            }
        
            _kitchenObjects = value;
            if (_kitchenObjects.HasKitchenObject())
            {
                Debug.LogError("Counter already has Kitchen Object");
            }

            value.KitchenObject = this;
            transform.parent = value.GetKitchenObjectControllerCounterTransform();
            transform.localPosition = Vector3.zero;
        }
    }

    public static KitchenObjectController SpawnKitchenObject(KitchenObjecsSO kitchenObjecsSo,
        IKitchenObjectsParent kitchenObjectsParent)
    {
        Transform kitchenObjectTransform = Instantiate(kitchenObjecsSo.prefab);
        KitchenObjectController kitchenObject = kitchenObjectTransform.GetComponent<KitchenObjectController>();
        kitchenObject.KitchenObjectsParent = kitchenObjectsParent;

        return kitchenObject;
    }

    public KitchenObjecsSO GetKitchenObjectSO()
    {
        return _kitchenObjecsSo;
    }

    public void DestroySelf()
    {
        KitchenObjectsParent.ClearKitchenObject();
        Destroy(gameObject);
    }
}
