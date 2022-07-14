#region

using System.Collections.Generic;
using UnityEngine;

#endregion

public class CheckPointSystem : MonoBehaviour
{
    private List<Vector3> _checkPointposition;
    private List<CheckPoint> _checkPoints;
    private CarCheckPointHelper[] _cars;
    private int _id;

    private const string CHECK_POINT_TAG = "CheckPoint";
    private const int CHECK_POINT_LAYER = 7;
    private const string WALL_TAG = "Wall";
    private const int WALL_LAYER = 6;
    public IReadOnlyList<Vector3> CheckPointPosition{ get { return _checkPointposition; } }


    public void InitSystem(CarCheckPointHelper[] cars, int numberOfLaps)
    {
        _checkPoints = new List<CheckPoint>();
        _checkPointposition = new List<Vector3>();
        _cars = cars;

        foreach (Transform note in transform)
        {
            foreach (Transform edgeObject in note)
            {
                if (edgeObject.TryGetComponent(out Wall wall))
                {
                    wall.gameObject.tag = WALL_TAG;
                    wall.gameObject.layer = WALL_LAYER;
                }

                foreach (Transform checkPoint in edgeObject)
                {
                    if (checkPoint.TryGetComponent(out CheckPoint temp))
                    {
                   
                        temp.OnCheckPointTrigger += OnCheckPointTrigger;
                        temp.SetId(_id);
                        temp.gameObject.tag = CHECK_POINT_TAG;
                        temp.gameObject.layer = CHECK_POINT_LAYER;
                        _id++;
                        _checkPoints.Add(temp);
                        _checkPointposition.Add(temp.transform.position);
                        //Debug.Log("Add " + temp.name + " From " + transform.name);
                    }
                }
            }
        }

        for (int i = 0; i < _cars.Length; i++)
        {
            _cars[i].SetCheckPointCount(_checkPoints.Count);
            _cars[i].SetNextCheckPoint(_checkPoints[0]);
            _cars[i].SetNumberOfLaps(numberOfLaps);
        }
    }

    private void OnCheckPointTrigger(CarCheckPointHelper car, CheckPoint checkPointId)
    {
        int nextCheckPointIndex = 0;
        
        if (car.NextCheckPoint == checkPointId)
        {
            Debug.Log(_checkPoints.IndexOf(checkPointId) + "id");
            Debug.Log(_checkPoints.Count);
            if (_checkPoints.IndexOf(checkPointId) + 1 == _checkPoints.Count)
            {
                car.SetNextCheckPoint(_checkPoints[0]);
                car.CompletedALap();
                return;
            }
            
            nextCheckPointIndex = _checkPoints.IndexOf(checkPointId) + 1;
            
            car.SetNextCheckPoint(_checkPoints[nextCheckPointIndex]);
            //Debug.Log(car.gameObject.name + " Move to CheckPoint " + checkPointId.name);
        }
        else
        {
            car.PassInCorrectCheckPoint();
            Debug.LogError(car.name + "Wrong CheckPoint" + checkPointId.ID);
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