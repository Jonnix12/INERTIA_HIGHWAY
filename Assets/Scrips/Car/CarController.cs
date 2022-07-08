using System;
using Unity.VisualScripting;
using UnityEngine;

public class CarController : CarEngineComponent
{
    #region Fields

    private float _accelerationInput;
    private float _steeringInput;
    private bool _isBreakingInput;

    private float _currentBreakForce;

    #endregion
    
    #region Updates

    private void Start()
    {
        InitSuspension();
    }

    private void FixedUpdate()
    {
        UpdateEngine(_accelerationInput);
        CalculateSteerackermannAngel(_steeringInput);

        if (_isBreakingInput)
        {
            for (int i = 0; i < Wheels.Length; i++)
            {
                Wheels[i].Brake();
            }
        }
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