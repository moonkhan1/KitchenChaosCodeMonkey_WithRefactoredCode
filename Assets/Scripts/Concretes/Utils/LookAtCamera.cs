using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private enum LookMode
    {
        LookAt,
        LookAtInverted,
        CameraForward,
        CameraForwardInverted
    }

    [SerializeField] private LookMode _mode;
    private Transform _transform;
    private Transform _cameraTransform;
    private void Awake()
    {
        _transform = GetComponent<Transform>();
        _cameraTransform = Camera.main?.transform;
    }

    private void LateUpdate()
    {
        switch (_mode)
        {
            case LookMode.LookAt:
                _transform.LookAt(_cameraTransform);
                break;
            case LookMode.LookAtInverted:
                Vector3 dirFromCamera = _transform.position - _cameraTransform.position;
                _transform.LookAt(_transform.position + dirFromCamera);
                break;
            case LookMode.CameraForward:
                _transform.forward = _cameraTransform.forward;
                break;
            case LookMode.CameraForwardInverted:
                _transform.forward = -_cameraTransform.forward;
                break;
        }
    }
}
