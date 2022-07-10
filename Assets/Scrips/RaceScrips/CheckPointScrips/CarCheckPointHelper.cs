#region

using System;
using UnityEngine;

#endregion

public class CarCheckPointHelper : MonoBehaviour, IComparable<CarCheckPointHelper>
{
    public event Action PassWrongCheckPass;
    public event Action OnPassCheckPoint;

    public event Action<CarCheckPointHelper> OnCompletedTheRace;

    [SerializeField] private int _racePosition;

    private CheckPoint _previousCheckPoint;
    private CheckPoint _nextCheckPoint;
    private int _checkPointCount;
    private int _numberOfCheckPointToEnd;
    private int _numberOfLaps;

    public int NumberOfCheckPointToEnd
    {
        get { return _numberOfCheckPointToEnd; }
    }

    public CheckPoint PreviousCheckPoint
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

    public void CompletedALap()
    {
        _numberOfLaps--;

        if (_numberOfLaps <= 0)
        {
            OnCompletedTheRace?.Invoke(this);
        }
    }


    public void SetRacePosition(int position)
    {
        _racePosition = position;
    }

    public void SetNumberOfLaps(int count)
    {
        _numberOfLaps = count;
    }

    public int CompareTo(CarCheckPointHelper otherCar)
    {
        if (_numberOfCheckPointToEnd > otherCar.NumberOfCheckPointToEnd)
        {
            return 1;
        }

        if (_numberOfCheckPointToEnd < otherCar.NumberOfCheckPointToEnd)
        {
            return -1;
        }

        float myDis = Vector3.Distance(transform.position, NextCheckPoint.transform.position);
        float otherDis = Vector3.Distance(otherCar.transform.position, otherCar.NextCheckPoint.transform.position);

        if (myDis > otherDis)
        {
            return 1;
        }

        if (myDis < otherDis)
        {
            return -1;
        }

        return 0;
    }
}