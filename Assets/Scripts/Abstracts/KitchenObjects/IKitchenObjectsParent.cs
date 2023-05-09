
using UnityEngine;

public interface IKitchenObjectsParent
{
    Transform GetKitchenObjectControllerCounterTransform();
    KitchenObjectController KitchenObject { get; set; }
    void ClearKitchenObject();
    bool HasKitchenObject();
}
