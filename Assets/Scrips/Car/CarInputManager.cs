using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarInputManager : MonoBehaviour
{
    #region ScrifsReference

    [SerializeField] private CarController _controller;
    private CarInput _input;

    #endregion

    private void Awake()
    {
        _input = new CarInput();
        CamaraFallowCar.SetTarget(_controller._cameraLookAT);
    }

    private void OnEnable()
    {
        _input.Enable();
    }

    private void OnDisable()
    {
        _input.Disable();
    }

    private void Update()
    {
        float acceleration = _input.Default.Acceleration.ReadValue<float>();
        float steer = _input.Default.Steering.ReadValue<float>();
        bool isBreak = _input.Default.Break.IsPressed();

        _controller.UpdateCarInputs(acceleration,steer,isBreak);

        CamaraFallowCar.IsLookBackInput = _input.CamControl.CamControl.IsPressed();
    }
}
