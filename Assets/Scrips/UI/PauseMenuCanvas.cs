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
        StartCoroutine(GameManager.Instance.LoadScene(scene.buildIndex, true));
        Time.timeScale = 1;
    }

    public void MoveToLevelSelection()
    {
        StartCoroutine(GameManager.Instance.LoadScene(2, true));
        Time.timeScale = 1;
    }

    public void MoveToMainMenu()
    {
        StartCoroutine(GameManager.Instance.LoadScene(1, true));
        Time.timeScale = 1;
    }

    private void UpdateFirstButton(GameObject firstButton)
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstButton);
    }
}
