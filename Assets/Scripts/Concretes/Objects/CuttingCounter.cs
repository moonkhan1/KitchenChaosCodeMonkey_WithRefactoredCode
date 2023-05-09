using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
   [SerializeField] private CutRecipeSO[] cutKitchenObjectArray;
   public event Action<float> OnProgress;
   public override event Action<string> OnAnimationPlay;
   private const string CUT_ANIMATION = "Cut";
   private int cuttingProgress;
   public override void Interact(PlayerController player)
   {
      if (!HasKitchenObject())
      {
         if (player.HasKitchenObject())
         {
            if (!HasRecipeWithInput(player.KitchenObject.GetKitchenObjectSO())) return;
            player.KitchenObject.KitchenObjectsParent = this;
            cuttingProgress = 0;
            CutRecipeSO cutRecipeSO = GetOutputFromInput(KitchenObject.GetKitchenObjectSO());
            OnProgress?.Invoke((float)cuttingProgress / cutRecipeSO.cuttingProgressMax);
            
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

   public override void InteractAlternate(PlayerController player)
   {
      if (HasKitchenObject() && HasRecipeWithInput(KitchenObject.GetKitchenObjectSO()))
      {
         cuttingProgress++;
         CutRecipeSO cutRecipeSO = GetOutputFromInput(KitchenObject.GetKitchenObjectSO());
         OnProgress?.Invoke((float)cuttingProgress / cutRecipeSO.cuttingProgressMax);
         OnAnimationPlay?.Invoke(CUT_ANIMATION);
         if (cuttingProgress >= cutRecipeSO.cuttingProgressMax)
         {
            KitchenObjecsSO outputKitchenObject = cutRecipeSO.output;
            KitchenObject.DestroySelf();
            KitchenObjectController.SpawnKitchenObject(outputKitchenObject, this);
         }

      }
   }

   private CutRecipeSO GetOutputFromInput(KitchenObjecsSO kitchenObjecsSO)
   {
      CutRecipeSO cutRecipeSo = cutKitchenObjectArray.First(u => u.input == kitchenObjecsSO);
      return cutRecipeSo;
   }

   private bool HasRecipeWithInput(KitchenObjecsSO inputKitchenObjecsSo)
   {
      if (cutKitchenObjectArray.Any(u => u.input == inputKitchenObjecsSo))
      {
         return true;
         
      }

      return false;
   }
}
