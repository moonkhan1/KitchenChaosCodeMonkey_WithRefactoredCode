using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StoveCounter : BaseCounter, IHasProgress
{
    public override event Action<bool> OnParticlePlay;
    public event Action<float> OnProgress;
    public enum State
    {
        Idle,
        Frying,
        Fried,
        Burned,
    }
    [SerializeField] private FryingRecipeSO[] _fryingRecipeSoArray;
    [SerializeField] private BurnedRecipeSO[] _burnedRecipeSoArray;
    private float _fryingTimer;
    private float _burningTimer;
    private FryingRecipeSO _fryingRecipeSo;
    private BurnedRecipeSO _burnedRecipeSo;
    private State _state;
    private void Update()
    {
        if (HasKitchenObject())
        {
            switch (_state)
            {
                case State.Idle:
                    OnParticlePlay?.Invoke(false);
                    break;
                case State.Frying:
                    _fryingTimer += Time.deltaTime;
                    OnProgress?.Invoke(_fryingTimer / _fryingRecipeSo.fryingTimerMax);
                    if (_fryingTimer >= _fryingRecipeSo.fryingTimerMax)
                    {
                        KitchenObject.DestroySelf();
                        KitchenObjectController.SpawnKitchenObject(_fryingRecipeSo.output, this);
                        _state = State.Fried;
                        _burningTimer = 0f;
                        _burnedRecipeSo = GetBurnedOutputFromInput(KitchenObject.GetKitchenObjectSO());
                        OnParticlePlay?.Invoke(true);
                    }
                    break;
                case State.Fried:
                    _burningTimer += Time.deltaTime;
                    OnProgress?.Invoke(_burningTimer / _burnedRecipeSo.burningTimerMax);
                    if (_burningTimer >= _burnedRecipeSo.burningTimerMax)
                    {
                        KitchenObject.DestroySelf();
                        KitchenObjectController.SpawnKitchenObject(_burnedRecipeSo.output, this);
                        _state = State.Burned;
                        OnParticlePlay?.Invoke(true);
                        OnProgress?.Invoke(0f);
                    }
                    break;
                case State.Burned:
                    OnParticlePlay?.Invoke(false);
                    break;
            }
        }

    }
    

    public override void Interact(PlayerController player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                if (!HasRecipeWithInput(player.KitchenObject.GetKitchenObjectSO())) return;
                player.KitchenObject.KitchenObjectsParent = this;
                _fryingRecipeSo = GetFriedOutputFromInput(KitchenObject.GetKitchenObjectSO());
                _state = State.Frying;
                _fryingTimer = 0f;
                OnParticlePlay?.Invoke(true);
                OnProgress?.Invoke(_fryingTimer / _fryingRecipeSo.fryingTimerMax);
            }
        }
        else
        {
            if (!player.HasKitchenObject())
            {
                KitchenObject.KitchenObjectsParent = player;
                _state = State.Idle;
                OnParticlePlay?.Invoke(false);
                OnProgress?.Invoke(0f);
            }
        }
    }
    
    private FryingRecipeSO GetFriedOutputFromInput(KitchenObjecsSO kitchenObjecsSO)
    {
        FryingRecipeSO fryingRecipeSo = _fryingRecipeSoArray.First(u => u.input == kitchenObjecsSO);
        return fryingRecipeSo;
    }
    private BurnedRecipeSO GetBurnedOutputFromInput(KitchenObjecsSO kitchenObjecsSO)
    {
        BurnedRecipeSO burnedRecipeSo = _burnedRecipeSoArray.First(u => u.input == kitchenObjecsSO);
        return burnedRecipeSo;
    }

    private bool HasRecipeWithInput(KitchenObjecsSO inputKitchenObjecsSo)
    {
        if (_fryingRecipeSoArray.Any(u => u.input == inputKitchenObjecsSo))
        {
            return true;
         
        }

        return false;
    }

 
}
