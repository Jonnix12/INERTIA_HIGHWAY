using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointSystem : MonoBehaviour
{   
    
    private List<CheckPoint> _checkPoints;
    
    private void Awake()
    {
        _checkPoints = new List<CheckPoint>();

        int GivenId = 0;
        
        foreach (Transform transform in transform)
        {
            CheckPoint temp;
            
            transform.TryGetComponent<CheckPoint>(out temp);
            
            temp.SetID(GivenId);
            GivenId++;
            
            temp.OnCheckPoineTrigger += OnCheckPoineTrigger;
            
            _checkPoints.Add(temp);
        }
    }

    private void OnCheckPoineTrigger(GameObject car,int checkPointId)
    {
        Debug.Log(car.name + checkPointId);
    }

    private void OnDisable()
    {
        foreach (var vaCheckPoint in _checkPoints)
        {
            vaCheckPoint.OnCheckPoineTrigger -= OnCheckPoineTrigger;
        }
    }
}
