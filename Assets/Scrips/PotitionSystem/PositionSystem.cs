using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PositionSystem : MonoBehaviour
{
    [SerializeField] private CheckPointSystem _checkPointSystem;
    private Dictionary<CarCheckPointHalper, int> _carsPositions;
    private int[] _ints;


    private CarCheckPointHalper[] _cars;

    private void Start()
    {
        _carsPositions = new Dictionary<CarCheckPointHalper, int>();
        _cars = _checkPointSystem.getCarCheckPointHalpers();
        
    }

    private void SortPosition()
    {
        for (int i = 0; i < _cars.Length; i++)
        {
            
        }
        
    }
}
