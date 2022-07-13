using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayCanvasManager : MonoBehaviour
{
    [Header("Car UI")]
    [SerializeField] private Canvas _miniMap;
    [SerializeField] private GameObject _speedUI;
    [Header("Stop Menu")]
    [SerializeField] private Canvas _pauseMenu;
    [Header("End game Menu")]
    [SerializeField] private Canvas _endRaceMenu;

    public void OpenStopMenu()
    {
        _miniMap.gameObject.SetActive(false);
        _speedUI.gameObject.SetActive(false);
        _pauseMenu.gameObject.SetActive(true);
        Time.timeScale = 0;
    }
    
    public void EndGame()
    {
        _miniMap.gameObject.SetActive(false);
        _speedUI.gameObject.SetActive(false);
        _endRaceMenu.gameObject.SetActive(true);
    }
    
    public void ResumeGame()
    {
        _pauseMenu.gameObject.SetActive(false);
        _miniMap.gameObject.SetActive(true);
        _speedUI.gameObject.SetActive(true);
        Time.timeScale = default;
    }

    public void Restart()
    {
        Scene scene = GameManager.Instance._sceneManager.GetActiveSecene();
        GameManager.Instance.SceneManager.LoadSceneAsync(scene.buildIndex, true);
    }

    public void MoveToLevelSelection()
    {
        
    }

    public void MoveToMainMenu()
    {

    }

    public void Quit()
    {
        Application.Quit();
    }
}
