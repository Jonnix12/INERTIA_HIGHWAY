#region

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#endregion

public class RaceManager : MonoBehaviour
{
    public event Action OnAllCarsCompleted;
    
    [SerializeField] private CheckPointSystem _checkPointSystem;
    [SerializeField] private TimeTrackSystem _timeTrackSystem;
    [SerializeField] private PositionSystem _positionSystem;

    [SerializeField] private int _numberOfLaps;

    [SerializeField] private GameObject bumble;
    [SerializeField] private GameObject bat;

    private List<Idisable> _carInputs;
    private List<CarRaceManager> _cars;
    
    public IReadOnlyList<CarRaceManager> Cars
    {
        get { return _cars; }
    }

    private void Awake()
    {
        if (GameManager.Instance.playerManager.ScelectCar == 1)
            bat.gameObject.SetActive(true);

        else
            bumble.gameObject.SetActive(true);

        WaitToLoad(5);
        CarRaceManager[] tempCars;
        tempCars = FindObjectsOfType<CarRaceManager>();

        _cars = new List<CarRaceManager>();

        for (int i = 0; i < tempCars.Length; i++)
        {
            _cars.Add(tempCars[i]);
        }

        List<CarCheckPointHelper> tempCarCheckPointHelpers = new List<CarCheckPointHelper>();

        List<TimeTrack> tempCarTrackTime = new List<TimeTrack>();

        _carInputs = new List<Idisable>();

        for (int i = 0; i < _cars.Count; i++)
        {
            if (_cars[i].gameObject.TryGetComponent(out CarCheckPointHelper tempCheckPointHelper))
            {
                tempCarCheckPointHelpers.Add(tempCheckPointHelper);
            }

            if (_cars[i].gameObject.TryGetComponent(out TimeTrack tempTimeTrack))
            {
                tempCarTrackTime.Add(tempTimeTrack);
            }

            if (_cars[i].gameObject.TryGetComponent(out Idisable carInput))
            {
                _carInputs.Add(carInput);
            }
        }

        CarCheckPointHelper[] tempArrayCheckPointHelpers = tempCarCheckPointHelpers.ToArray();
        TimeTrack[] tempArrayTimeTracks = tempCarTrackTime.ToArray();

        _checkPointSystem.InitSystem(tempArrayCheckPointHelpers, _numberOfLaps);
        _positionSystem.InitSystem(tempArrayCheckPointHelpers);
        _timeTrackSystem.InitSystem(tempArrayTimeTracks);
        StartRace();
    }

    public IEnumerator WaitToLoad(int time)
    {
        yield return new WaitForSeconds(time);
    }

    public void EndRace(CarRaceManager carRaceManager)
    {
        if (carRaceManager.CarCheckPointHelper.IsPlayer)
        {
            
        }
        
        for (int i = 0; i < _cars.Count; i++)
        {
            if (!_cars[i].IsDone)
            {
                return;
            }
        }
            
        //allCarsDane
    }

    private void StartRace()
    {
        _timeTrackSystem.StartRace(_carInputs.ToArray());
    }
}