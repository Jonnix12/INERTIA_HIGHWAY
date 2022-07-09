using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceManager : MonoBehaviour
{
   [SerializeField] private CheckPointSystem _checkPointSystem;
   [SerializeField] private TimeTrackSystem _timeTrackSystem;
   [SerializeField] private PositionSystem _positionSystem;

   private List<CarInputManager> _carInputs;
   private List<GameObject> _cars;

   private void Awake()
   {
      GameObject[] tempCars;
      tempCars = GameObject.FindGameObjectsWithTag("Car");

      _cars = new List<GameObject>();
      
      for (int i = 0; i < tempCars.Length; i++)
      {
         _cars.Add(tempCars[i]);
      }
   }

   private void Start()
   {
      List<CarCheckPointHelper> tempCarCheckPointHelpers = new List<CarCheckPointHelper>();
      
      List<TimeTrack> tempCarTrackTime = new List<TimeTrack>();

      _carInputs = new List<CarInputManager>();

      for (int i = 0; i < _cars.Count; i++)
      {
         if (_cars[i].TryGetComponent<CarCheckPointHelper>(out CarCheckPointHelper tempCheckPointHelper))
         {
            tempCarCheckPointHelpers.Add(tempCheckPointHelper);
         }

         if (_cars[i].TryGetComponent<TimeTrack>(out TimeTrack tempTimeTrack))
         {
            tempCarTrackTime.Add(tempTimeTrack);
         }

         if (_cars[i].TryGetComponent<CarInputManager>(out CarInputManager carInput))
         {
            _carInputs.Add(carInput);
         }
      }

      CarCheckPointHelper[] tempArrayCheckPointHelpers = tempCarCheckPointHelpers.ToArray();
      TimeTrack[] tempArrayTimeTracks = tempCarTrackTime.ToArray();
      
      _checkPointSystem.InitSystem(tempArrayCheckPointHelpers);
      _positionSystem.InitSystem(tempArrayCheckPointHelpers);
      _timeTrackSystem.InitSystem(tempArrayTimeTracks);
      StartRace();
   }

   private void StartRace()
   {
      _timeTrackSystem.StartRace(_carInputs.ToArray());
   }
}
