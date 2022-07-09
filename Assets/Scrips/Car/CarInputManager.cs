using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class CarInputManager : MonoBehaviour
{
    #region ScrifsReference

    [SerializeField] private CarController _controller;
    public CarInput Input;
    private float acceleration;
    public float CarAcceleration { get { return acceleration; } }
    #endregion

    private void Awake()
    {
        Input = new CarInput();
        Input.Disable();
    }
    
    public void Update()
    {
        acceleration = Input.Default.Acceleration.ReadValue<float>();
        float steer = Input.Default.Steering.ReadValue<float>();
        bool isBreak = Input.Default.Break.IsPressed();
        _controller.UpdateCarInputs(acceleration,steer,isBreak);

        CamaraFallowCar.IsLookBackInput = Input.CamControl.CamControl.IsPressed();
    }
    [ContextMenu("EnableInput")]
    private void ForceEnable()
    {
        EnableInput(true);
    }

    public void EnableInput(bool stats)
    {
        if (stats)
        {
            Input.Enable();
        }
        else
        {
            Input.Disable();
        }
    }
}
