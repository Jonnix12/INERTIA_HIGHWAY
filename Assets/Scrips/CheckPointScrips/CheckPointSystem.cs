using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointSystem : MonoBehaviour
{
    private List<CheckPoint> _checkPoints;
    private CarCheckPointHalper[] _cars;

    private void Awake()
    {
        _checkPoints = new List<CheckPoint>();

        _cars = FindObjectsOfType<CarCheckPointHalper>();
        
        foreach (Transform transform in transform)
        {
            CheckPoint temp;
            
            transform.TryGetComponent<CheckPoint>(out temp);
            
            temp.OnCheckPointTrigger += OnCheckPointTrigger;
            
            _checkPoints.Add(temp);
        }
    }

    private void Start()
    {
        for (int i = 0; i < _cars.Length; i++)
        {
            _cars[i].SetNextCheckPoint(_checkPoints[0]);
        }
    }

    private void OnCheckPointTrigger(CarCheckPointHalper car,CheckPoint checkPointId)
    {
        if (car.NextCheckPoint == checkPointId)
        {
            int nextCheckPointIndex;
            
            if (_checkPoints.IndexOf(checkPointId) + 1 > _checkPoints.Count)
            {
                nextCheckPointIndex = 0;
            }
            else
            {
                nextCheckPointIndex = _checkPoints.IndexOf(checkPointId) + 1;
            }
            
            car.SetNextCheckPoint(_checkPoints[nextCheckPointIndex]);
            Debug.Log(car.gameObject.name + " Move to CheckPoint " + checkPointId.name);
        }
        else
        {
            Debug.Log("Wrong CheckPoint");
        }
        
    }

    private void OnDisable()
    {
        foreach (var vaCheckPoint in _checkPoints)
        {
            vaCheckPoint.OnCheckPointTrigger -= OnCheckPointTrigger;
        }
    }
}
