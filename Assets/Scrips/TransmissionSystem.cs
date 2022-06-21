using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TransmissionSystem : CarSteeringSystem_V2
{
    #region Fields

    //2016 Chevrolet Camaro Preliminary

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

    public int Gear
    {
        get { return _currentGear; }
    }

    #endregion

    #region ProtectedFunctions

    protected void UpdateTransmission(float engineForce,float temp)
    {
        AddForceToWheel(engineForce);
        _engineRpm = temp;
        //CalculateEngineRpm();
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

     private void AddForceToWheel(float engineForce)
    {
        if (_engineRpm < 6000)
        {
            for (var i = 0; i < 4; i++) //set only for the two rear wheels
                Wheels[i].AddWheelForce(CalculateMotorForce(_currentGear,engineForce) / 2f);
        }
        else
        {
            for (var i = 2; i < 4; i++) //set only for the two rear wheels
                Wheels[i].AddWheelForce(CalculateMotorForce(_currentGear,-0.25f) / 2f);
        }
        
        Debug.Log(CalculateMotorForce(_currentGear,engineForce));
    }

    private float CalculateMotorForce(int gear,float input)
    {
        return input / GetGearRatio(_currentGear) * FINAL_DRIVE_RATIO;
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
    // private void CalculateEngineRpm()
    // {
    //     float leftWheelSpeed;
    //     float rightWheelSpeed;
    //     
    //     leftWheelSpeed = Wheels[2].WheelRPM * GetGearRatio(_currentGear) * FINAL_DRIVE_RATIO;
    //     rightWheelSpeed = Wheels[3].WheelRPM * GetGearRatio(_currentGear) * FINAL_DRIVE_RATIO;
    //
    //     _engineRpm = Mathf.Clamp((leftWheelSpeed + rightWheelSpeed) / 2, _minRpm, _maxRpm);
    //     
    // }
    //
    // //Vehicle speed = Wheels RPM × Tire diameter × π × 60 / 1000
    // private void CalculateCarSpeed()
    // {
    //     float totalWheelSpeed = 0;
    //
    //     List<float> wheelRpm = new List<float>(4);
    //
    //     for (int i = 0; i < wheelRpm.Capacity; i++)
    //     {
    //         float temp = Wheels[i].WheelRPM * 0.4f * Mathf.PI * 60 / 1000;
    //         
    //         wheelRpm.Add(temp);
    //     }
    //
    //     for (int i = 0; i < wheelRpm.Capacity; i++)
    //     {
    //         totalWheelSpeed += wheelRpm[i];
    //     }
    //     
    //     _carSpeed = totalWheelSpeed / 4;
    // }

    #endregion
   
}
