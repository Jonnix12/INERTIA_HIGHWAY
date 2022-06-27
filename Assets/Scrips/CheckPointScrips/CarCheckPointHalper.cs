using UnityEngine;

public class CarCheckPointHalper : MonoBehaviour
{
    private CheckPoint _previousCheckPoint;
    private CheckPoint _nextCheckPoint;

    public CheckPoint PreviousCheckPoint
    {
        get { return _previousCheckPoint; }
    }

    public CheckPoint NextCheckPoint
    {
        get { return _nextCheckPoint; }
    }
    
    public void SetNextCheckPoint(CheckPoint nextCheckPoint)
    {
        _previousCheckPoint = _nextCheckPoint;
        _nextCheckPoint = nextCheckPoint;
    }
}
