using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CarController : CarSteeringSystem
{
    #region Fields
    
    [Header("Motor Parameters")] 
    [SerializeField]
    private float _motorForce;
    [SerializeField]
    private float _breakForce;
    [SerializeField]
    private float _maxAngel;
    
    [Header("Camera LookAT")]
    public Transform _cameraLookAT;
    
    private float _accelerationInput;
    private bool _isBreakingInput;
    
    private float _currentBreakForce;
    private float[] _fowerdSlips;
    private float[] _sideSlips;

    #endregion

    #region Prop

    //CamaraProp

    #endregion

    #region UnityCallback

    private void Start()
    {
        InitSteeringSystem();
        
        for (int i = 0; i < _wheels.Length; i++)
        {
            _wheels[i].InhitWheel(i);
        }
    }

    #endregion

    #region Updates
    private void FixedUpdate()
    {
        AddForceToWheel(_wheels,_accelerationInput * _motorForce);
        _currentBreakForce = _isBreakingInput ? _breakForce : 0f;
        AddBrackForceToWheel(_wheels,_currentBreakForce * _breakForce);

        UpdateWheelSteerAngel();
        
        for (int i = 0; i < _wheels.Length; i++)
        {
            _wheels[i].UpdateWheel();
        }
    }

    #endregion

    #region PublicFuncation

    public void UpdateCarInputs(float acceleration,float steer, bool isBreak)
    {
        _accelerationInput = acceleration;
        GetSteeringInput(steer);
        _isBreakingInput = isBreak;
    }

    #endregion
    
    #region PrivateFuncation

    private void AddForceToWheel(WheelStract[] wheelColliders, float motorForce)
    {
        for (int i = 2; i < 4; i++)//set only for the two rear wheels
        {
            _wheels[i].Collider.motorTorque = motorForce;
        }
    }
    
    private void AddBrackForceToWheel(WheelStract[] wheelColliders, float brackForce)
    {
        for (int i = 0; i < wheelColliders.Length; i++)
        {
            _wheels[i].Collider.brakeTorque = brackForce;
        }
    }

    #endregion
}
