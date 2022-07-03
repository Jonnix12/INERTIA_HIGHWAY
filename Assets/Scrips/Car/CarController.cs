using System;
using UnityEngine;

public class CarController : CarEngineComponent
{
    #region Fields

    [Header("Camera LookAT")] public Transform _cameraLookAT;

    private float _accelerationInput;
    private float _steeringInput;
    private bool _isBreakingInput;

    private float _currentBreakForce;

    #endregion
    
    #region Updates

    private void FixedUpdate()
    {
        UpdateEngine(_accelerationInput);
        CalculateSteerackermannAngel(_steeringInput);
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
    
}