using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class CarEngineComponent : CarSteeringSystem_V2
{
    [SerializeField] private Animator _animator;
    
    [Header("Engine Parameters:")]
    [SerializeField] private AnimationCurve _engineCurve;
    [SerializeField] private float _smoothDampTime;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _speedLimitMultiplier;
    
    private const int MAX_RPM = 6500;

    private float _carSpeed;
    
    private float _engineRpm;
    
    private float _refVelosty = 0;
    
    private float _tempInput = 0;

    private float _waitFor = 0.2f;
    private WaitForSeconds _waitForSpeedCalculate;

    public float EngineRpm
    {
        get { return _engineRpm; }
    }

    public float speed
    {
        get
        {
            return _carSpeed;
        }
    }
    private void Awake()
    {
        _waitForSpeedCalculate = new WaitForSeconds(_waitFor);
        StartCoroutine(SpeedCalculate());
    }

    protected void UpdateEngine(float input)
    {
        for (int i = 0; i < Wheels.Length; i++)
        {
            Wheels[i].AddWheelForce(CalculateMotorForce(input),CalculateSpeedMultiplier());
            Wheels[i].UpdateWheelVisal();
            AdjustTurnRadius(_carSpeed);
        }
        UpdateWheelRotation(_carSpeed/_maxSpeed);
    }
    
        private float CalculateMotorForce(float input)
    {
        _tempInput = Mathf.SmoothDamp(_tempInput, Mathf.Abs(input), ref _refVelosty, _smoothDampTime);
        _engineRpm = Mathf.Lerp(1000, MAX_RPM, _tempInput);
        
        if (input > 0)
        {
            return _engineCurve.Evaluate(_engineRpm / MAX_RPM);
        }
        if (input < 0)
        {
            return -_engineCurve.Evaluate(_engineRpm / MAX_RPM);
        }

        return 0;
    }

    private float CalculateSpeedMultiplier()
    {
        float temp = _speedLimitMultiplier * (-_carSpeed / -_maxSpeed);
        temp = Mathf.Clamp(temp, 1, 5);
        return temp;
    }
       
    private void UpdateWheelRotation(float speedPrecedent)
    {
        _animator.speed = speedPrecedent;
    }
    
    private IEnumerator SpeedCalculate()
    {
        while (true)
        {
            Vector3 stratPos = transform.position;
            yield return _waitForSpeedCalculate;
            Vector3 endPos = transform.position;

            float distanceTraveled = Vector3.Distance(stratPos, endPos);
            _carSpeed = (distanceTraveled/ _waitFor) * 8.6f;
            //Debug.Log("Car speed " + _carSpeed);
        }
    }
}