using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CarHud : MonoBehaviour
{
    [SerializeField] private TimeTrack _timeTrack;
    [SerializeField] private CarCheckPointHelper _checkPoint;
    
    [SerializeField] private TextMeshProUGUI _time;
    [SerializeField] private TextMeshProUGUI _position;
    [SerializeField] private TextMeshProUGUI _countDown;
    [SerializeField] private TextMeshProUGUI _numOfLapRemain;
    [SerializeField] private TextMeshProUGUI _maxLaps;

    private void Start()
    {
        TimeTrackSystem.CountDown += GetCountDown;
        _maxLaps.text = _checkPoint.NumberOfLaps.ToString();
    }

    private void OnDisable()
    {
        TimeTrackSystem.CountDown -= GetCountDown;
    }

    private void Update()
    {
        float tempTime = _timeTrack.LapTime;
        _time.text = tempTime.ToString("0.00");
        _position.text = _checkPoint.RacePosition.ToString();
    }

    private void GetCountDown(int secondRemain)
    {
        _countDown.text = secondRemain.ToString();

        if (secondRemain < 1)
        {
            _countDown.text = "GO!";
            StartCoroutine(Timer());
        }
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(1);
        _countDown.enabled = false;
    }
}
