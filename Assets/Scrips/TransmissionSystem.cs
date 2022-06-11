using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class TransmissionSystem : CarSteeringSystem
{
    #region Fields

    //2016 Chevrolet Camaro Preliminary
    [Header("Transmission System Parameters")]
    [SerializeField] private float _motorForce;

    [SerializeField] private int _maxRpm;
    [SerializeField] private int _minRpm;
    
    private const float FINAL_DRIVE_RATIO = 3.72f;
    private const int NUMBER_OF_GEARS = 6;
    
    private int _currentGear;

    private float _currentMotorForce;
    private float _engineRpm;
    private float _carSpeed;

    #endregion

    #region Props

    public float EngineRpm
    {
        get { return _engineRpm; }
    }

    public float CarSpeed
    {
        get { return _carSpeed; }
    }

    #endregion

    #region ProtectedFunctions

    protected void UpdateTransmission(float input)
    {
        AddForceToWheel(input);
        CalculateEngineRpm();
        CalculateCarSpeed();
    }

    #endregion

    #region PublicFunctions

    public void UpShifter(InputAction.CallbackContext callbackContext)
    {
        if (_currentGear < NUMBER_OF_GEARS)
        {
            _currentGear++;
        }
        else
        {
            Debug.Log("Top Gear");
        }
    }
    
    public void DawnShifter(InputAction.CallbackContext callbackContext)
    {
        if (_currentGear > 0)
        {
            _currentGear--;
        }
        else
        {
            Debug.Log("Low Gear");
        } 
    }

    #endregion

    #region PrivateFuncations

     private void AddForceToWheel(float input)
    {
        if (_engineRpm < 6000)
        {
            for (var i = 2; i < 4; i++) //set only for the two rear wheels
                Wheels[i].Collider.motorTorque = CalculateMotorForce(_currentGear,input) / 2f;
        }
        else
        {
            for (var i = 2; i < 4; i++) //set only for the two rear wheels
                Wheels[i].Collider.motorTorque = CalculateMotorForce(_currentGear,-0.25f) / 2f;
        }
    }

    private float CalculateMotorForce(int gear,float input)
    {
        _currentMotorForce = _motorForce * GetGearRatio(gear) * input;
       
        return _currentMotorForce;
    }

    private float GetGearRatio(int gear)
    {
        switch (gear)
        {
            case 1: 
                return 2.66f;
            case 2:
                return 1.78f;
            case 3:
                return 1.3f;
            case 4:
                return 1;
            case 5:
                return 0.7f;
            case 6:
                return 0.5f;
            default:
                return 0f;
        }
    }
    

    //RPM = Wheels RPM * Transmission ratio * Final drive ratio
    private void CalculateEngineRpm()
    {
        float leftWheelSpeed;
        float rightWheelSpeed;
        
        leftWheelSpeed = Wheels[2].WheelRPM * GetGearRatio(_currentGear) * FINAL_DRIVE_RATIO;
        rightWheelSpeed = Wheels[3].WheelRPM * GetGearRatio(_currentGear) * FINAL_DRIVE_RATIO;

        _engineRpm = Mathf.Clamp((leftWheelSpeed + rightWheelSpeed) / 2, _minRpm, _maxRpm);
        
    }

    //Vehicle speed = Wheels RPM × Tire diameter × π × 60 / 1000
    private void CalculateCarSpeed()
    {
        float totalWheelSpeed = 0;

        List<float> wheelRpm = new List<float>(4);

        for (int i = 0; i < wheelRpm.Capacity; i++)
        {
            float temp = Wheels[i].WheelRPM * 0.4f * Mathf.PI * 60 / 1000;
            
            wheelRpm.Add(temp);
        }

        for (int i = 0; i < wheelRpm.Capacity; i++)
        {
            totalWheelSpeed += wheelRpm[i];
        }
        
        _carSpeed = totalWheelSpeed / 4;
    }

    #endregion
   
}
