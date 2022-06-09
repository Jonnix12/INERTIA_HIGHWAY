using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSteeringSystem : MonoBehaviour
{
    [Header("Wheels")] 
    [SerializeField] protected WheelStract[] _wheels;

    [SerializeField] private float _maxSteerAngel;
    
    [SerializeField] private Transform _leftAnchor;
    [SerializeField] private Transform _rightAnchor;
    
    private float _steerInput;

    private Vector3 _leftAnchorBasePosition;
    private Vector3 _rightAnchorBasePosition;
    private Vector3 _leftAnchorTarget;
    private Vector3 _rightAnchorTarget;

    private Vector3 _leftDefaultWheelSteerDir;
    private Vector3 _rightDefaultWheelSteerDir;

    private float leftInput;
    private float rightInput;
    
    public WheelStract[] Wheels
    {
        get { return _wheels; }
    }

    private void Awake()
    {
        WheelHalper.onSetDir += GetWheelDirVector;
    }

    protected void InitSteeringSystem()
    {
        _leftAnchorBasePosition = _leftAnchor.localPosition;
        _rightAnchorBasePosition = _rightAnchor.localPosition;
        _leftAnchorTarget = new Vector3(_leftAnchor.position.x, _leftAnchor.position.y, -_maxSteerAngel);
        _rightAnchorTarget = new Vector3(_rightAnchor.position.x, _rightAnchor.position.y, -_maxSteerAngel);
    }

    protected void GetSteeringInput(float input)
    {
        if (input < 0)
        {
            leftInput = Mathf.Abs(input);
        }
        else if (input > 0)
        {
            rightInput = input;
        }
        else
        {
            rightInput = 0;
            leftInput = 0; 
        }
    }

    private void MoveAnchorPosition()
    {
        _leftAnchor.localPosition = Vector3.Lerp(_leftAnchorBasePosition, _leftAnchorTarget, leftInput); 
        _rightAnchor.localPosition = Vector3.Lerp(_rightAnchorBasePosition, _rightAnchorTarget, rightInput);
    }

    protected void UpdateWheelSteerAngel()
    {
        MoveAnchorPosition();
        
        for (int i = 0; i < 2; i++)//set only for the front wheels
        {
            _wheels[i].Collider.steerAngle = CalculateSteerAngel(_rightAnchor.position, i);
        }
    }

    private float CalculateSteerAngel(Vector3 target, int index)
    {
        Vector3 dir = target - _wheels[index].Transform.localPosition;
        
        float angel = Vector3.Angle(_wheels[index].DefaultWheelSteerDir , dir.normalized);
        
        return angel;
    }

    private void OnDrawGizmosSelected()
    {
        for (int i = 0; i < _wheels.Length; i++)
        {
            Ray ray = new Ray(_wheels[i].Collider.transform.position, _wheels[i].DefaultWheelSteerDir);
            Gizmos.DrawRay(ray);
        }
    }

    private void GetWheelDirVector(Vector3 dir, int index)
    {
        _wheels[index].DefaultWheelSteerDir = dir;
    }
}
