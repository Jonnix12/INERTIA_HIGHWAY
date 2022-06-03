using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CarController : MonoBehaviour
{
    #region ScrifsReference
    
    [SerializeField] private CarInputManager _input;
    
    #endregion
    
    #region Fields
    
    [Header("Motor Parameters")] 
    [SerializeField]
    private float _motorForce;
    [SerializeField]
    private float _breakForce;
    [SerializeField]
    private float _angelSpeed;

    [Header("Colliders")]
    [SerializeField] private WheelCollider _frontLeftCollider;
    [SerializeField] private WheelCollider _frontRightCollider;
    [SerializeField] private WheelCollider _rearLeftCollider;
    [SerializeField] private WheelCollider _rearRightCollider;
    
    [Header("Transforms")]
    [SerializeField] private Transform _frontLeftTransform;
    [SerializeField] private Transform _frontRightTransform;
    [SerializeField] private Transform _rearLeftTransform;
    [SerializeField] private Transform _rearRightTransform;

    private float _accelerationInput;
    private float _steerInput;
    private float _currentBreakForce;
    private bool _isBreaking;
    
    #endregion

    #region Updates

    #region Inputs

    private void Update()
    {
        _accelerationInput = _input.Input.Default.Acceleration.ReadValue<float>();
        _steerInput = _input.Input.Default.Steering.ReadValue<float>();
        _isBreaking = _input.Input.Default.Break.IsPressed();
        
        #region GFXUpdate
        
        UpdateWheelVisal(_rearLeftCollider,_rearLeftTransform);
        UpdateWheelVisal(_rearRightCollider,_rearRightTransform);
        UpdateWheelVisal(_frontLeftCollider,_frontLeftTransform);
        UpdateWheelVisal(_frontRightCollider,_frontRightTransform);
        
        #endregion
        
    }

    #endregion

    #region Physics

    private void FixedUpdate()
    {
        Acceleration();
        HandlSteering();
        _currentBreakForce = _isBreaking ? _breakForce : 0f;
        Break();
    }

    #endregion
  

    #endregion
    
    #region PrivateFuncation

    private void Break()
    {
        AddBrackForceToWheel(_rearLeftCollider,_currentBreakForce);
        AddBrackForceToWheel(_rearRightCollider,_currentBreakForce);
        AddBrackForceToWheel(_frontLeftCollider,_currentBreakForce);
        AddBrackForceToWheel(_frontRightCollider,_currentBreakForce);
    }

    private void Acceleration()
    {
        AddForceToWheel(_rearLeftCollider, _accelerationInput * _motorForce);
        AddForceToWheel(_rearRightCollider, _accelerationInput * _motorForce);
        AddForceToWheel(_frontLeftCollider, _accelerationInput * _motorForce);
        AddForceToWheel(_frontRightCollider, _accelerationInput * _motorForce);
    }

    private void HandlSteering()
    {
        AddSteerAngelToWheel(_frontLeftCollider,_steerInput * _angelSpeed);
        AddSteerAngelToWheel(_frontRightCollider,_steerInput * _angelSpeed);
    }

    private void AddSteerAngelToWheel(WheelCollider wheelCollider, float angel)
    {
        wheelCollider.steerAngle = angel;
    }
    
    private void AddForceToWheel(WheelCollider wheelCollider, float motorForce)
    {
        wheelCollider.motorTorque = motorForce;
    }
    
    private void AddBrackForceToWheel(WheelCollider wheelCollider, float brackForce)
    {
        wheelCollider.brakeTorque = brackForce;
    }

    private void UpdateWheelVisal(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        
        wheelCollider.GetWorldPose(out pos,out rot);

        wheelTransform.position = pos;
        wheelTransform.rotation = rot;
    }

    #endregion

   
}
