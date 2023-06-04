using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : SingletonBase<MusicManager>
{
    private const string PLAYER_PREFS_MUSIC_VOLUME = "MusicVolume";
    private AudioSource _audioSource;
    public float Volume { get; private set; } = 0.3f;

    private void Awake()
    {
        MakeSingleton(this);
        _audioSource = GetComponent<AudioSource>();
        Volume = PlayerPrefs.GetFloat(PLAYER_PREFS_MUSIC_VOLUME, 0.3f);
        _audioSource.volume = Volume;
    }

    public void ChangeVolume()
    {
        Volume += 0.1f;
        if (Volume > 1f)
        {
            Volume = 0f;
        }

        _audioSource.volume = Volume;
        PlayerPrefs.SetFloat(PLAYER_PREFS_MUSIC_VOLUME, Volume);
        PlayerPrefs.Save();
    }
}
