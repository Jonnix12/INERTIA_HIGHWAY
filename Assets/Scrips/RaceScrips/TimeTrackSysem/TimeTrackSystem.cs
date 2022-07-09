using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTrackSystem : MonoBehaviour
{
    public event Action OnStartRace;

    [SerializeField] private int _countDownSeconds;
    private int _secondsRemain;
    
    private TimeTrack[] _carsTimer;
    private WaitForSeconds _waitForOneCSecond;

    private bool isRaceStart = false;
    
    public void InitSystem(TimeTrack[] cars)
    {
        _carsTimer = cars;
        
        for (int i = 0; i < _carsTimer.Length; i++)
        {
            OnStartRace += _carsTimer[i].StartRaceTimer;
        }
        
        _waitForOneCSecond = new WaitForSeconds(1);
        _secondsRemain = _countDownSeconds;
    }

    public void StartRace(Idisable[] carInputManagers)
    {
        StartCoroutine(RaceCountDown(carInputManagers));
    }

    private IEnumerator RaceCountDown(Idisable[] carInputManagers)
    { 
        for (int i = 0; i < _countDownSeconds; i++)
        {
                yield return _waitForOneCSecond;
                _secondsRemain--;

                if (_secondsRemain == 0)
                {
                    for (int j = 0; j < carInputManagers.Length; j++)
                    {
                        carInputManagers[j].EnableInput(true);
                    }
                    OnStartRace?.Invoke();
                }
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < _carsTimer.Length; i++)
        {
            OnStartRace -= _carsTimer[i].StartRaceTimer;
        }
    }
}
