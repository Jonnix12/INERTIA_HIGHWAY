using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseMenuCanvas : MonoBehaviour
{
    [SerializeField]
    GameObject firstButton;
    private void Start()
    {
        UpdateFirstButton(firstButton);
    }
    public void Restart()
    {
        Scene scene = GameManager.Instance.SceneManager.GetActiveSecene();
        GameManager.Instance.LoadScene(scene.buildIndex);
        Time.timeScale = 1;
    }

    public void MoveToLevelSelection()
    {
        GameManager.Instance.LoadScene(2);
        Time.timeScale = 1;
    }

    public void MoveToMainMenu()
    {
        GameManager.Instance.LoadScene(1);
        Time.timeScale = 1;
    }

    private void UpdateFirstButton(GameObject firstButton)
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstButton);
    }
}
