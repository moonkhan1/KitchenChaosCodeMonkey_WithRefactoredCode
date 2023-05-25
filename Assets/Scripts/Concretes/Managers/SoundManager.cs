using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundManager : SingletonBase<SoundManager>
{
    [SerializeField] private AudioClipsSoArrays _audioClipsSoArrays;

    private void Awake()
    {
        MakeSingleton(this);
    }

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManagerOnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManagerOnRecipeFailed;
        CuttingCounter.OnCut += CuttingCounterOnCut;
        PlayerController.Instance.OnPickupSomething += PlayerOnPickupSomething;
        TrashCounter.OnThrow += TrashCounterOnThrow;
        BaseCounter.OnKitchenObjectPlacedHere += BaseCounterOnObject;
    }

    private void BaseCounterOnObject(BaseCounter baseCounter)
    {
        PlaySound(_audioClipsSoArrays.objectPickUps, baseCounter.transform.position);
    }

    private void TrashCounterOnThrow(TrashCounter trashCounter)
    {
        PlaySound(_audioClipsSoArrays.trash, trashCounter.transform.position);
    }
    

    private void PlayerOnPickupSomething()
    {
        PlaySound(_audioClipsSoArrays.objectPickUps, PlayerController.Instance.transform.position);
    }

    private void CuttingCounterOnCut(CuttingCounter cuttingCounter)
    {
        PlaySound(_audioClipsSoArrays.chop, cuttingCounter.transform.position);
    }

    private void DeliveryManagerOnRecipeFailed()
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(_audioClipsSoArrays.deliveryFail, deliveryCounter.transform.position);
    }

    private void DeliveryManagerOnRecipeSuccess()
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(_audioClipsSoArrays.deliverySuccess, deliveryCounter.transform.position);
    }

    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volume = 1f)
    {
        AudioClip audioClip = audioClipArray[Random.Range(0, audioClipArray.Length)];
        PlaySound(audioClip, position, volume);
    }
    public void PlayFootstepsSound(Vector3 position, float volume)
    {
        PlaySound(_audioClipsSoArrays.footsteps, position, volume);
    }
    private void PlaySound(AudioClip audioClip, Vector3 position, float volume = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }


    
}



