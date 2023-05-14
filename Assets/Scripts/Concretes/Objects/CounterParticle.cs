using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CounterParticle : MonoBehaviour
{
    [SerializeField] private BaseCounter _baseCounter;
    [SerializeField] private List<GameObject> _gameObjectParticles;

    private void Start()
    {
        _baseCounter.OnParticlePlay += BaseCounterOnOnParticlePlay;
    }

    private void BaseCounterOnOnParticlePlay(bool condition)
    {
        _gameObjectParticles.ForEach(u=>u.SetActive(condition));
    }
}
