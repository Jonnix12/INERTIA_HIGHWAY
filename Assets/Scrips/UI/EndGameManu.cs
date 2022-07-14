using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndGameManu : MonoBehaviour
{
    [SerializeField] private RaceManager _raceManager;
    
    [SerializeField] private TextMeshProUGUI[] _names;
    
    [SerializeField] private TextMeshProUGUI[] _times;

    private void Start()
    {
        _names = new TextMeshProUGUI[4];
        _times = new TextMeshProUGUI[4];

        _raceManager.OnAllCarsCompleted += SetManu;
    }

    private void OnDisable()
    {
        _raceManager.OnAllCarsCompleted -= SetManu;
    }

    public void SetManu()
    {
        for (int i = 0; i < _raceManager.Cars.Count; i++)
        {
            _names[i].text = _raceManager.Cars[i].Name;
            _times[i].text = _raceManager.Cars[i].TimeOfLap.ToString();
        }
        
        gameObject.SetActive(true);
    }
}
