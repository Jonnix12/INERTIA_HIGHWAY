using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class CarEngineComponent : TransmissionSystem
{
    [SerializeField] private AnimationCurve _engineCurve;
    [SerializeField] private float _smoothDampTime;
    
    private const int MAX_RPM = 6500;

    private float _engineRpm;
    
    private float _refVelosty = 0;
    float _tempInput = 0;

    public float EngineRpm
    {
        get { return _engineRpm; }
    }

    protected void UpdateEngine(float input)
    {
        UpdateTransmission(CalculateMotorForce(input));
    }
    
    private float CalculateMotorForce(float input)
    {
        _tempInput = Mathf.SmoothDamp(_tempInput, Mathf.Abs(input), ref _refVelosty, _smoothDampTime);
        _engineRpm = Mathf.Lerp(0, MAX_RPM, _tempInput);
        

        if (input > 0)
        {
            return _engineCurve.Evaluate(_engineRpm / MAX_RPM);
        }
        if (input < 0)
        {
            return -_engineCurve.Evaluate(_engineRpm / MAX_RPM);
        }

        return 0;
    }
}