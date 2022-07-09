using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PositionSystem : MonoBehaviour
{
    private CarCheckPointHelper[] _cars;

    public void InitSystem(CarCheckPointHelper[] cars)
    {
        _cars = cars;
    }

    private void SortPosition()
    {
        Array.Sort(_cars);
        SendRacePositionToCars();
    }

    private void Update()
    {
        SortPosition();
    }

    private void SendRacePositionToCars()
    {
        for (int i = 0; i < _cars.Length; i++)
        {
            _cars[i].SetRacePosition(i+1);
        }
    }
}
