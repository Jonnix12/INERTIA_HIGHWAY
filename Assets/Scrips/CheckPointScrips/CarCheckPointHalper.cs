using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCheckPointHalper : MonoBehaviour
{
    private int _currentCheckPoint = 0;
    private int _nextCheckPoint = 1;
 
    public void NextCheckPoint(int checkPointId)
    {
        if (checkPointId == _nextCheckPoint)
        {
            _currentCheckPoint++;
            _nextCheckPoint = _currentCheckPoint + 1;
            Debug.Log("Good");
        }
        else
        {
            Debug.Log("Worng CheckPoint");
        }
    }
}
