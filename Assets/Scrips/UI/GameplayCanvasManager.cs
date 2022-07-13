using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayCanvasManager : MonoBehaviour
{
    [Header("Car UI")]
    private Canvas _miniMap;
    private Canvas _speedUI;
    [Header("Stop Menu")]
    private Canvas _pauseMenu;
    [Header("End game Menu")]
    private Canvas _endRaceMenu;

    public void StopMenu()
    {
        _miniMap.gameObject.SetActive(false);
        _speedUI.gameObject.SetActive(false);
        _pauseMenu.gameObject.SetActive(true);
    }
    
    public void EndGame()
    {
        _miniMap.gameObject.SetActive(false);
        _speedUI.gameObject.SetActive(false);
        _endRaceMenu.gameObject.SetActive(true);
    }
}
