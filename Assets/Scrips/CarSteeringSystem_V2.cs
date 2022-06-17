using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSteeringSystem_V2 : MonoBehaviour
{
    [Header("Wheels")] 
    [SerializeField] private WheelStruct[] _wheels;
    
    [Header("Car Specs")]
    [SerializeField]
    private float wheelBase;

    [SerializeField] private float rearTranck;
    [SerializeField] private float turnRadius;

    private float ackermannAngelLeft;
    private float ackermannAngelRight;

    public float AckermannAngelLeft
    {
        get { return ackermannAngelLeft; }
    }

    public float AckermannAngelRight
    {
        get { return ackermannAngelRight; }
    }
    
    public WheelStruct[] Wheels => _wheels;

    protected void CalculateSteerackermannAngel(float input)
    {
        if (input > 0)
        {
            ackermannAngelLeft = Mathf.Rad2Deg * Mathf.Atan(wheelBase / (turnRadius + (rearTranck / 2))) * input;
            ackermannAngelRight = Mathf.Rad2Deg * Mathf.Atan(wheelBase / (turnRadius - (rearTranck / 2))) * input;
        }
        else if (input < 0)
        {
            ackermannAngelLeft = Mathf.Rad2Deg * Mathf.Atan(wheelBase / (turnRadius - (rearTranck / 2))) * input;
            ackermannAngelRight = Mathf.Rad2Deg * Mathf.Atan(wheelBase / (turnRadius + (rearTranck / 2))) * input;
        }
        else
        {
            ackermannAngelLeft = 0;
            ackermannAngelRight = 0;
        }
        
        _wheels[0].Collider.steerAngle = ackermannAngelLeft;
        _wheels[1].Collider.steerAngle = ackermannAngelRight;

    }
}
