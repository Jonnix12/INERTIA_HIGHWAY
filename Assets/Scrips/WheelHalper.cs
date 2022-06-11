using System;
using UnityEngine;

public class WheelHalper : MonoBehaviour
{
    public static event Action<Vector3,int,Transform> onSetDir;
    
    [SerializeField] private int _wheelIndex;
    [SerializeField] private bool _isToTheRight;
    
    private Vector3 _dir;

    void Start()
    {
        if (_isToTheRight)
        {
            _dir = transform.right;
            onSetDir?.Invoke(_dir,_wheelIndex,transform);
        }
        else
        {
            _dir = -transform.right;
            onSetDir?.Invoke(_dir,_wheelIndex,transform);
        }
       
    }
    
}
