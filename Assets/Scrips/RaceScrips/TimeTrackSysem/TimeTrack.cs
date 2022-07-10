#region

using System.Collections;
using UnityEngine;

#endregion

public class TimeTrack : MonoBehaviour
{
    private float _trackLapTime;

    private readonly bool _isRaceing = true;

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