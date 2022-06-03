using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class CamaraFallowCar : MonoBehaviour
{
    [SerializeField] private CarInputManager _inputManager;
    [SerializeField] private Vector3 _offSet;
    [SerializeField] private float _timeToSmoothDamp = 1f;
    [SerializeField] private Transform _traget;

    private Vector3 _velocity = Vector3.zero;
    
    void Start()
    {
        transform.position = _traget.position + _offSet;
        
    }
    
    void FixedUpdate()
    {
        Vector3 tragetPosition = _traget.position + (_traget.rotation * _offSet);
        Vector3 camPosition = transform.position;

        transform.position = Vector3.SmoothDamp(camPosition, tragetPosition, ref _velocity,_timeToSmoothDamp * Time.deltaTime);
        
        transform.LookAt(_traget,Vector3.up);
        
        ChanageLook();
    }

    private void ChanageLook()
    {
        bool isLookBack = _inputManager.Input.CamControl.CamControl.IsPressed();
        
        _offSet.z = isLookBack ? 6 : -6; //need Work
    }
}
