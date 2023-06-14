using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundManager : SingletonBase<SoundManager>
{
    private const string PLAYER_PREFS_SOUND_EFFECTS_VOLUME = "SoundEffectsVolume";
    [SerializeField] private AudioClipsSoArrays _audioClipsSoArrays;
    public float Volume { get; private set; } = 1f;

    private void Awake()
    {
        MakeSingleton(this);
        Volume = PlayerPrefs.GetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, 1f);
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

    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volumeMultiplayer = 1f)
    {
        AudioClip audioClip = audioClipArray[Random.Range(0, audioClipArray.Length)];
        PlaySound(audioClip, position, volumeMultiplayer);
    }
   
    private void PlaySound(AudioClip audioClip, Vector3 position, float volumeMultiplayer = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volumeMultiplayer * Volume);
    }
    public void PlayFootstepsSound(Vector3 position, float volume)
    {
        PlaySound(_audioClipsSoArrays.footsteps, position, volume);
    }

    public void PlayCountdownSound()
    {
        PlaySound(_audioClipsSoArrays.warning, Vector3.zero);
    }
    public void PlayWarningSound(Vector3 position)
    {
        PlaySound(_audioClipsSoArrays.warning, position);
    }
    public void ChangeVolume()
    {
        Volume += 0.1f;
        if (Volume > 1f)
        {
            Volume = 0f;
        }
        PlayerPrefs.SetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, Volume);
        PlayerPrefs.Save();
    }
    
}



