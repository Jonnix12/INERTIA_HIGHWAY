using UnityEngine;

public class CarCheckPointHalper : MonoBehaviour
{
    private CheckPoint _currentCheckPoint;
    private CheckPoint _nextCheckPoint;

    public CheckPoint CurrentCheckPoint
    {
        get { return _currentCheckPoint; }
    }

    public CheckPoint NextCheckPoint
    {
        get { return _nextCheckPoint; }
    }
    
    public void SetNextCheckPoint(CheckPoint nextCheckPoint)
    {
        _currentCheckPoint = _nextCheckPoint;
        _nextCheckPoint = nextCheckPoint;
    }
}
