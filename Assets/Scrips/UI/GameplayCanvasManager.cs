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
    [SerializeField] private Canvas _bumblePauseMenu;
    [Header("End game Menu")]
    [SerializeField] private Canvas _endRaceMenu;

    private bool _isPaused;

    private void Awake()
    {
        _batPauseMenu.gameObject.SetActive(false);
        _bumblePauseMenu.gameObject.SetActive(false);
        _endRaceMenu.gameObject.SetActive(false);
        _miniMap.gameObject.SetActive(true);
        _speedUI.gameObject.SetActive(true);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.JoystickButton9))
        {
            if (!_isPaused)
                OpenStopMenu();

            else
                ResumeGame();
        }
    }

    public void OpenStopMenu()
    {
        _miniMap.gameObject.SetActive(false);
        _speedUI.gameObject.SetActive(false);
        if(GameManager.Instance.playerManager.ScelectCar==1)
        _batPauseMenu.gameObject.SetActive(true);
        else
            _bumblePauseMenu.gameObject.SetActive(true);
        Time.timeScale = 0;
    }
    
    public void ResumeGame()
    {
        _miniMap.gameObject.SetActive(true);
        _speedUI.gameObject.SetActive(true);
        if (GameManager.Instance.playerManager.ScelectCar == 1)
            _batPauseMenu.gameObject.SetActive(false);
        else
            _bumblePauseMenu.gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
