using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public abstract class SingletonBase<T> : NetworkBehaviour where T : Component
{
    public static T Instance { get; private set; }

    protected void MakeSingleton(T instance)
    {
        if (Instance == null)
        {
            Instance = instance;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
