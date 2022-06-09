using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarInputManager : MonoBehaviour
{
    #region ScrifsReference

    [SerializeField] private CarController _controller;
    public CarInput Input;

    #endregion

    private void Awake()
    {
        Input = new CarInput();
        CamaraFallowCar.SetTarget(_controller._cameraLookAT);
    }

    private void OnEnable()
    {
        Input.Enable();
        
    }

    private void OnDisable()
    {
        Input.Disable();
    }

    private void Update()
    {
        float acceleration = Input.Default.Acceleration.ReadValue<float>();
        float steer = Input.Default.Steering.ReadValue<float>();
        bool isBreak = Input.Default.Break.IsPressed();

        _controller.UpdateCarInputs(acceleration,steer,isBreak);
       
       CamaraFallowCar.IsLookBackInput = Input.CamControl.CamControl.IsPressed();
    }
}
