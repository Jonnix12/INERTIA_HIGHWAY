using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TimeTrack : MonoBehaviour
{
    private float _trackLapTime = 0;
   
    private bool _isRaceing = true;

    public float LapTime
    {
        get { return _trackLapTime; }
    }
    private void Start()
    {
    }

    public void StartRaceTimer()
    {
        StartCoroutine(RaceTiner());
    }

    private IEnumerator RaceTiner()
    {
        while (_isRaceing)
        {
            _trackLapTime += Time.deltaTime;
            yield return null;
        }
    }
}
