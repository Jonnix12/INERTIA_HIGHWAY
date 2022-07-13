using System.Collections;
using System.Collections.Generic;
using Mono.Cecil.Cil;
using UnityEngine;

public class CarRaceManager : MonoBehaviour
{
    [SerializeField] private TimeTrack _timeTrack;
    [SerializeField] private CarCheckPointHelper _checkPointHelper;

    [SerializeField] private string _name;

    public CarCheckPointHelper CarCheckPointHelper
    {
        get { return _checkPointHelper; }
    }
    
    public float TimeOfLap
    {
        get { return _timeTrack.LapTime; }
    }

    public string Name
    {
        get { return _name; }
    }
}
