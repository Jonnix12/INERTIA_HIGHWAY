#region

using UnityEngine;

#endregion

public class CarSteeringSystem_V2 : CarSuspensionSystem
{
    [Header("Car Specs")] [SerializeField] private float _wheelBase;
    [SerializeField] private float _rearTranck;
    [SerializeField] private float _turnRadius;

    private float _currentTurnRadius;

    private float _ackermannAngelLeft;
    private float _ackermannAngelRight;

    public float AckermannAngelLeft
    {
        get { return _ackermannAngelLeft; }
    }

    public float AckermannAngelRight
    {
        get { return _ackermannAngelRight; }
    }


    protected void AdjustTurnRadius(float speed)
    {
        if (speed > 40)
        {
            float tempSpeed = Mathf.Clamp(speed, 0, 180);
            _currentTurnRadius = Mathf.Lerp(_turnRadius, _turnRadius * 5, tempSpeed / 180);
        }
        else
        {
            _currentTurnRadius = _turnRadius;
        }
    }

    protected void CalculateSteerackermannAngel(float input)
    {
        if (input > 0)
        {
            _ackermannAngelLeft = Mathf.Rad2Deg * Mathf.Atan(_wheelBase / (_currentTurnRadius + (_rearTranck / 2))) *
                                  input;
            _ackermannAngelRight = Mathf.Rad2Deg * Mathf.Atan(_wheelBase / (_currentTurnRadius - (_rearTranck / 2))) *
                                   input;
        }
        else if (input < 0)
        {
            _ackermannAngelLeft = Mathf.Rad2Deg * Mathf.Atan(_wheelBase / (_currentTurnRadius - (_rearTranck / 2))) *
                                  input;
            _ackermannAngelRight = Mathf.Rad2Deg * Mathf.Atan(_wheelBase / (_currentTurnRadius + (_rearTranck / 2))) *
                                   input;
        }
        else
        {
            _ackermannAngelLeft = 0;
            _ackermannAngelRight = 0;
        }

        Wheels[0].SetWheelAngel(_ackermannAngelLeft);
        Wheels[1].SetWheelAngel(_ackermannAngelRight);
    }
}