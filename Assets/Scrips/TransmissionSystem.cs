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
    private const float FINAL_DRIVE_RATIO = 3.72f;
    private const int NUMBER_OF_GEARS = 6;
    
    private int _currentGear;
    private float _currnetInput;
    private float _wheelRpm;
    
    private float _carSpeed;

    #endregion

    #region Props
    

    public float CarSpeed
    {
        get { return _carSpeed; }
    }

    public int CurrentGear
    {
        get { return _currentGear; }
    }

    #endregion

    #region ProtectedFunctions

    protected void UpdateTransmission(float engineRpm,float engineTorque)
    {
        AddForceToWheel(engineRpm,engineTorque);
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

     private void AddForceToWheel(float engineRpm, float engineTorque)//Work
    {
        for (var i = 2; i < 4; i++) //set only for the two rear wheels
            Wheels[i].Collider.motorTorque = CalculateMotorForce(engineRpm,engineTorque);
    }
    
     
     //Torque (N.m) = 9.5488 x Power (kW) / Speed (RPM)
    private float CalculateMotorForce(float engineRpm,float engineTorque)
    {
        _wheelRpm = engineRpm / GetGearRatio(_currentGear) / FINAL_DRIVE_RATIO;
        float force = engineTorque * GetGearRatio(_currentGear) * FINAL_DRIVE_RATIO / 4f;
        //float force = 9.5488f * 339 / _wheelRpm;
        
        return force;
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
