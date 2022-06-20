using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSteeringSystem_V2 : CarSuspensionSystem
{
    [Header("Car Specs")]
    [SerializeField] private float wheelBase;
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

    protected void AdjustTurnRadius(float speed)
    {
      
    }

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
        
        Wheels[0].SetWheelAngel(ackermannAngelLeft);
        Wheels[1].SetWheelAngel(ackermannAngelRight);

    }
}
