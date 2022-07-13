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
    
    public void ResumeGame()
    {
        _pauseMenu.gameObject.SetActive(false);
        _miniMap.gameObject.SetActive(true);
        _speedUI.gameObject.SetActive(true);
        Time.timeScale = default;
    }

    public void EndGame()
    {
        _miniMap.gameObject.SetActive(false);
        _speedUI.gameObject.SetActive(false);
        _endRaceMenu.gameObject.SetActive(true);
    }

    public void Restart()
    {
        Scene scene = GameManager.Instance.SceneManager.GetActiveSecene();
        StartCoroutine(GameManager.Instance.LoadScene(scene.buildIndex,false));
    }

    public void MoveToLevelSelection()
    {
        StartCoroutine(GameManager.Instance.LoadScene(2, false));
        
    }

    public void MoveToMainMenu()
    {
        StartCoroutine(GameManager.Instance.LoadScene(1, false));
    }

    public void Quit()
    {
        Application.Quit();
    }
}
