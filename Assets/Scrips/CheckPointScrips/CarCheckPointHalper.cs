using System;
using UnityEngine;

public class CarCheckPointHalper : MonoBehaviour
{
    public event Action PassWrongCheckPass;
    public event Action OnPassCheckPoint;
    private CheckPoint _previousCheckPoint;
    private CheckPoint _nextCheckPoint;
    private int _checkPointCount = 0;
    private int _numberOfCheckPointToEnd = 0;

    public  CheckPoint PreviousCheckPoint
    {
        get { return _previousCheckPoint; }
    }

    public CheckPoint NextCheckPoint
    {
        get { return _nextCheckPoint; }
    }

    public Transform carTrasform { get; internal set; }

    public void SetNextCheckPoint(CheckPoint nextCheckPoint)
    {
        OnPassCheckPoint?.Invoke();
        _previousCheckPoint = _nextCheckPoint;
        _nextCheckPoint = nextCheckPoint;
        _numberOfCheckPointToEnd--;
    }
    public void PassInCorrectCheckPoint()
    {
        PassWrongCheckPass?.Invoke();
    }

    public void SetCheckPointCount(int count)
    {
        _checkPointCount = count;
        _numberOfCheckPointToEnd = _checkPointCount;
    }
    
}
