using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEngineComponent : TransmissionSystem
{
    [SerializeField] private AnimationCurve _engineCurve;
    [SerializeField] private float _smoothDampTime;
    
    private const int MAX_RPM = 6500;
    private const int MIN_RPM = 1000;
    private const int MAX_ENGINE_TORQUE = 617;
    private const int MIN_ENGINE_TORQUE = 0;  
     
    private float _engineRpm;
    private float _currentEngineTorque;
    private float _refVelosty = 0;
    float _tempInput = 0;

    public float EngineRpm
    {
        get { return _engineRpm; }
    }

    protected void UpdateEngine(float input)
    {
        CalculateMotorForce(input);
        UpdateTransmission(_engineRpm,_currentEngineTorque);
    }
    
    private void CalculateMotorForce(float input)
    {
        _tempInput = Mathf.SmoothDamp(_tempInput, input, ref _refVelosty, _smoothDampTime);
        float temp = _engineCurve.Evaluate(_tempInput);
        _engineRpm = Mathf.Lerp(MIN_RPM, MAX_RPM, _tempInput);
        _currentEngineTorque = Mathf.Lerp(MIN_ENGINE_TORQUE, MAX_ENGINE_TORQUE, temp);
    }
}
