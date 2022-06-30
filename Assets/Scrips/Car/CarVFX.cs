using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CarVFX : MonoBehaviour
{
    [SerializeField] private CarController _controller;
    
    [SerializeField] private ParticleSystem _smoke;
    [SerializeField] private ParticleSystem _dust;
    [SerializeField] private float _timeToWaitForParticle = 2f;
    private WaitForSeconds _waitFor;

    private bool _isCoroutineRuning = false;

    private void Start()
    {
        _waitFor = new WaitForSeconds(_timeToWaitForParticle);
    }

    void Update()
    {
       _smoke.Play();
    }

    private IEnumerator ParticleTimeOut()
    {
        _isCoroutineRuning = true;
        yield return _waitFor;
        _isCoroutineRuning = false;
        _smoke.Stop();
    }
}
