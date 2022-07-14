#region

using System.Collections;
using UnityEngine;

#endregion

public class TimeTrack : MonoBehaviour
{
    private float _trackLapTime;

    private  bool _isRaceing = true;

    public float LapTime
    {
        get { return _trackLapTime; }
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

    public void StopTime()
    {
        _isRaceing = false;
    }
}