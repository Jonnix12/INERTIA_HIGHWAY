using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayCanvasManager : MonoBehaviour
{
    [Header("Car UI")]
    [SerializeField] private GameObject _miniMap;
    [SerializeField] private GameObject _speedUI;
    [Header("Stop Menu")]
    [SerializeField] private Canvas _batPauseMenu;
    [SerializeField] private Canvas _BumblePauseMenu;
    [Header("End game Menu")]
    [SerializeField] private Canvas _endRaceMenu;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OpenStopMenu();
        }
    }

    public void OpenStopMenu()
    {
        _miniMap.gameObject.SetActive(false);
        _speedUI.gameObject.SetActive(false);
        _batPauseMenu.gameObject.SetActive(true);
        Time.timeScale = 0;
    }
    
    public void ResumeGame()
    {
        _miniMap.gameObject.SetActive(true);
        _speedUI.gameObject.SetActive(true);
        _batPauseMenu.gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
