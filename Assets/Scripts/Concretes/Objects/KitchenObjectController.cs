using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class KitchenObjectController : MonoBehaviour
{
    [FormerlySerializedAs("_kitchenObjecsSo")] [SerializeField] private KitchenObjectsSO kitchenObjectsSo;

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

    public static KitchenObjectController SpawnKitchenObject(KitchenObjectsSO kitchenObjectsSo,
        IKitchenObjectsParent kitchenObjectsParent)
    {
        Transform kitchenObjectTransform = Instantiate(kitchenObjectsSo.prefab);
        KitchenObjectController kitchenObject = kitchenObjectTransform.GetComponent<KitchenObjectController>();
        kitchenObject.KitchenObjectsParent = kitchenObjectsParent;

        return kitchenObject;
    }

    public bool TryGetPlate(out PlateKitchenObject plateKitchenObject)
    {
        if (this is PlateKitchenObject)
        {
            plateKitchenObject = this as PlateKitchenObject;
            return true;
        }
        plateKitchenObject = null;
        return false;
    }

    public KitchenObjectsSO GetKitchenObjectSO()
    {
        return kitchenObjectsSo;
    }

    public void DestroySelf()
    {
        KitchenObjectsParent.ClearKitchenObject();
        Destroy(gameObject);
    }
}
