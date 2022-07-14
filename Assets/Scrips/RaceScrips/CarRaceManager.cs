using System;
using System.Collections;
using System.Collections.Generic;
using Mono.Cecil.Cil;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEngine;

public class CarRaceManager : MonoBehaviour
{
    public event Action<CarRaceManager> CarCompletedTheRace;
    
    [SerializeField] private TimeTrack _timeTrack;
    [SerializeField] private CarCheckPointHelper _checkPointHelper;
    [SerializeField] private CarControl _carControl;
    
    [SerializeField] private string _name;
    private bool _isDone;
            
    public CarCheckPointHelper CarCheckPointHelper
    {
        get { return _checkPointHelper; }
    }

    public bool IsDone
    {
        get { return _isDone; }
    }
    
    public float TimeOfLap
    {
        get { return _timeTrack.LapTime; }
    }

    public string Name
    {
        get { return _name; }
    }


    private void Start()
    {
        _checkPointHelper.OnCompletedTheRace += CarFinished;
        _isDone = false;
    }

    private void OnDisable()
    {
        _checkPointHelper.OnCompletedTheRace -= CarFinished;
    }

    public void CarFinished(CarCheckPointHelper checkPointHelper)
    {
        if (checkPointHelper.IsPlayer)
        {
            _carControl.OnEndRace();
        }

        _timeTrack.StopTime();
        _isDone = true;
        CarCompletedTheRace?.Invoke(this);
    }
    
    
}
