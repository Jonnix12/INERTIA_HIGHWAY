using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PositionSystem : MonoBehaviour
{
    [SerializeField] private CheckPointSystem _checkPointSystem;
    private Dictionary<CarCheckPointHelper, int> _carsPositions;
    private int[] _ints;

    private CarCheckPointHelper[] _cars;


    private void Start()
    {
        _carsPositions = new Dictionary<CarCheckPointHelper, int>();
        _cars = _checkPointSystem.getCarCheckPointHalpers();
        
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
