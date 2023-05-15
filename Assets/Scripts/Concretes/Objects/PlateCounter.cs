using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCounter : BaseCounter
{
    [SerializeField] private KitchenObjecsSO plateKitchenObjectSO;
    private readonly float plateSpawnTimeMax = 4f;
    private float _plateSpawnTimer;
    private int _plateSpawnAmount;
    private readonly int plateSpawnMaxAmount = 4;

    public event Action OnPlateSpawn; 
    public event Action OnPlateRemoved; 

    private void Update()
    {
        _plateSpawnTimer += Time.deltaTime;
        if (_plateSpawnTimer > plateSpawnTimeMax)
        {
            _plateSpawnTimer = 0f;

            if (_plateSpawnAmount < plateSpawnMaxAmount)
            {
                _plateSpawnAmount++;
                OnPlateSpawn?.Invoke();
            }
        }
    }

    public override void Interact(PlayerController player)
    {
        if (!player.HasKitchenObject())
        {
            if (_plateSpawnAmount > 0)
            {
                _plateSpawnAmount--;
                KitchenObjectController.SpawnKitchenObject(plateKitchenObjectSO, player);
                OnPlateRemoved?.Invoke();
            }
        }
    }
}
