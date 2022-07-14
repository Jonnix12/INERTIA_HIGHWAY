using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EndGameManu : MonoBehaviour
{
    [SerializeField] private RaceManager _raceManager;
    [SerializeField] private GameObject _firstButton;
    
    private TextMeshProUGUI[] _names;
    
    private TextMeshProUGUI[] _times;

    private void Start()
    {
        _names = new TextMeshProUGUI[4];
        _times = new TextMeshProUGUI[4];

        _raceManager.OnAllCarsCompleted += SetManu;
        UpdateFirstButton(_firstButton);
    }

    private void OnDisable()
    {
        _raceManager.OnAllCarsCompleted -= SetManu;
    }

    public void SetManu()
    {
        for (int i = 0; i < _raceManager.Cars.Count; i++)
        {
            _names[i].text = _raceManager.Cars[i].name;
            _times[i].text = _raceManager.Cars[i].TimeOfLap.ToString();
        }
        
        gameObject.SetActive(true);
    }

    public void NextRace()
    {
         Scene scene = GameManager.Instance.SceneManager.GetActiveSecene();
        if(scene.buildIndex==5)
            StartCoroutine(GameManager.Instance.LoadScene(1,false));

        else
        StartCoroutine(GameManager.Instance.LoadScene(scene.buildIndex + 1, false)); 
    }

    public void MainMenu()
    {
        StartCoroutine(GameManager.Instance.LoadScene(1, false)); 
    }

    private void UpdateFirstButton(GameObject firstButton)
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstButton);
    }
}
