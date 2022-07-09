using System;
using UnityEngine;

public class CarCheckPointHelper : MonoBehaviour , IComparable<CarCheckPointHelper>
{
    public event Action PassWrongCheckPass;
    public event Action OnPassCheckPoint;
    
    [SerializeField] private int _racePosition = 0;
    
    private CheckPoint _previousCheckPoint;
    private CheckPoint _nextCheckPoint;
    private int _checkPointCount = 0;
    private int _numberOfCheckPointToEnd = 0;

    public int NumberOfCheckPointToEnd
    {
        get { return _numberOfCheckPointToEnd; }
    }

    public  CheckPoint PreviousCheckPoint
    {
        get { return _previousCheckPoint; }
    }

    public CheckPoint NextCheckPoint
    {
        get { return _nextCheckPoint; }
    }
    
    public int RacePosition
    {
        get { return _racePosition; }
    }

    public void SetNextCheckPoint(CheckPoint nextCheckPoint)
    {
        OnPassCheckPoint?.Invoke();
        _previousCheckPoint = _nextCheckPoint;
        _nextCheckPoint = nextCheckPoint;
        _numberOfCheckPointToEnd--;
        
        if (_numberOfCheckPointToEnd <= 0)
        {
            _numberOfCheckPointToEnd = _checkPointCount;
        }
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


    public void SetRacePosition(int position)
    {
        _racePosition = position;
    }

    public int CompareTo(CarCheckPointHelper otherCar)
    {
        if (_numberOfCheckPointToEnd > otherCar.NumberOfCheckPointToEnd)
        {
            return 1;
        }
        else if(_numberOfCheckPointToEnd < otherCar.NumberOfCheckPointToEnd)
        {
            return -1;
        }
        else
        {
            float myDis = Vector3.Distance(transform.position, NextCheckPoint.transform.position);
            float otherDis = Vector3.Distance(otherCar.transform.position, otherCar.NextCheckPoint.transform.position);

            if (myDis > otherDis)
            {
                return 1;
            }
            else if(myDis < otherDis)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }
    }
}
