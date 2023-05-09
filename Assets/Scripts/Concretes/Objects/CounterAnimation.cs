using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterAnimation : MonoBehaviour
{
    private Animator _animator;
    [SerializeField] private BaseCounter _baseCounter;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _baseCounter.OnAnimationPlay += ContainerCounterOnOnPlayerGetObject;
    }

    private void ContainerCounterOnOnPlayerGetObject(string variable)
    {
        _animator.SetTrigger(variable);
    }
}
