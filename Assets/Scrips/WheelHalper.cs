using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelHalper : MonoBehaviour
{
    public static event Action<Vector3,int> onSetDir;
    
    [SerializeField] private int _wheelIndex;
    [SerializeField] private bool _isToTheRight;
    
    private Vector3 _dir;

    void Start()
    {
        if (_isToTheRight)
        {
            _dir = transform.right;
            onSetDir?.Invoke(_dir,_wheelIndex);
        }
        else
        {
            _dir = -transform.right;
            onSetDir?.Invoke(_dir,_wheelIndex);
        }
       
    }
    
}
