using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class CarEngineComponent : CarSteeringSystem_V2
{
    [Header("Engine Parameters:")]
    [SerializeField] private AnimationCurve _engineCurve;
    [SerializeField] private float _smoothDampTime;
    
    private const int MAX_RPM = 6500;

    private float _carSpeed;
    
    private float _engineRpm;
    
    private float _refVelosty = 0;
    
    private float _tempInput = 0;

    private float _waitFor = 0.2f;
    private WaitForSeconds _waitForSpeedCalculate;

    public float CarSpeed
    {
        get { return _carSpeed; }
    }
    
    public float EngineRpm
    {
        get { return _engineRpm; }
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
            if (input != 0 && _carSpeed < 180)
            {
                Wheels[i].AddWheelForce(CalculateMotorForce(input));
                AdjustTurnRadius(_carSpeed);
            }
            else if (input == 0 && _carSpeed > 40)
            {
                Wheels[i].AddWheelForce(CalculateMotorForce(-0.25f));
            }
            else
            {
                Wheels[i].AddWheelForce(CalculateMotorForce(0));
            }
        }
    }
    
        private float CalculateMotorForce(float input)
    {
        _tempInput = Mathf.SmoothDamp(_tempInput, Mathf.Abs(input), ref _refVelosty, _smoothDampTime);
        _engineRpm = Mathf.Lerp(0, MAX_RPM, _tempInput);
        

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

    private IEnumerator SpeedCalculate()
    {
        while (true)
        {
            Vector3 stratPos = transform.position;
            yield return _waitForSpeedCalculate;
            Vector3 endPos = transform.position;

            float distanceTraveled = Vector3.Distance(stratPos, endPos);
            _carSpeed = (distanceTraveled/ _waitFor) * 8.6f;
            //Debug.Log(_carSpeed);
        }
    }
}