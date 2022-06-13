using System;
using UnityEngine;

public class CarController : CarEngineComponent
{
    #region Fields
    
    [Header("Center Of Mass")]
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private Transform _centerOfMass;
    
    [Header("Motor Parameters")]
    
    [SerializeField] private float _breakForce;

    [Header("Camera LookAT")] public Transform _cameraLookAT;

    private float _accelerationInput;
    private float _steeringInput;
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
        _rb.centerOfMass = _centerOfMass.position;
    }

    #endregion

    #region Updates

    private void FixedUpdate()
    {
        UpdateEngine(_accelerationInput);

        _currentBreakForce = _isBreakingInput ? _breakForce : 0f;
        AddBrackForceToWheel(Wheels, _currentBreakForce * _breakForce);
        
        GetSteeringInput(_steeringInput);
        UpdateWheelSteerAngel();

        for (var i = 0; i < Wheels.Length; i++) Wheels[i].UpdateWheel();
    }

    #endregion

    #region PublicFuncation

    public void UpdateCarInputs(float acceleration, float steer, bool isBreak)
    {
        _accelerationInput = acceleration;
        _steeringInput = steer;
        _isBreakingInput = isBreak;
    }

    
    #endregion
    
    #region PrivateFuncation
    
    private void AddBrackForceToWheel(WheelStruct[] wheelColliders, float brackForce)
    {
        for (var i = 0; i < wheelColliders.Length; i++) Wheels[i].Collider.brakeTorque = brackForce;
    }

    #endregion
}