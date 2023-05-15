using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCounterVisual : MonoBehaviour
{
    [SerializeField] private PlateCounter _plate;
    [SerializeField] private Transform _plateTopPoint;
    [SerializeField] private Transform _platePrefab;

    private List<Transform> _plateVisualTransformList;

    private void Awake()
    {
        _plateVisualTransformList = new List<Transform>();
    }

    private void Start()
    {
        _plate.OnPlateSpawn += OnPlateSpawn;
        _plate.OnPlateRemoved += OnPlateRemoved;
    }

    private void OnPlateRemoved()
    {
        Transform plateVisualTransform = _plateVisualTransformList[_plateVisualTransformList.Count - 1];
        _plateVisualTransformList.Remove(plateVisualTransform);
        Destroy(plateVisualTransform.gameObject);
    }

    private void OnPlateSpawn()
    {
        Transform plateVisualTransform = Instantiate(_platePrefab, _plateTopPoint);

        const float plateOffsetY = 0.08f;
        plateVisualTransform.localPosition = new Vector3(0f, plateOffsetY * _plateVisualTransformList.Count, 0f);
        _plateVisualTransformList.Add(plateVisualTransform);
    }
}
