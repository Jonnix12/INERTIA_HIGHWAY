using System;
using UnityEngine;

public class CarController : TransmissionSystem
{
    #region Fields
    
    [Header("Center Of Mass")]
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private Transform _centerOfMass;

    [Header("Motor Parameters")] [SerializeField]
    private float _inputSmoothDamp;
    [SerializeField] private float _breakForce;

    [Header("Camera LookAT")] public Transform _cameraLookAT;

    private float refVelosity = 0;
    
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
        
    }

    #endregion

    #region Updates

    private void FixedUpdate()
    {
        UpdateTransmission(_accelerationInput);

        _currentBreakForce = _isBreakingInput ? _breakForce : 0f;
        AddBrackForceToWheel(Wheels, _currentBreakForce * _breakForce);

        for (var i = 0; i < Wheels.Length; i++) Wheels[i].UpdateWheel();
    }
    

    #endregion

    #region PublicFuncation

    public void UpdateCarInputs(float acceleration, float steer, bool isBreak)
    {
        _accelerationInput = Mathf.SmoothDamp(_accelerationInput, acceleration, ref refVelosity, _inputSmoothDamp);
        _accelerationInput = acceleration;
        CalculateSteerackermannAngel(steer);
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