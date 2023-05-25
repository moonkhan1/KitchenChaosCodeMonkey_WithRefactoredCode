using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Sounds", menuName = "Sounds/Audio Clip Arrays", order = 51)]
public class AudioClipsSoArrays : ScriptableObject
{
    public AudioClip[] chop;
    public AudioClip[] deliveryFail;
    public AudioClip[] deliverySuccess;
    public AudioClip[] footsteps;
    public AudioClip[] objectDrop;
    public AudioClip[] objectPickUps;
    public AudioClip[] trash;
    public AudioClip[] warning;
    public AudioClip[] stoveSound;
}
