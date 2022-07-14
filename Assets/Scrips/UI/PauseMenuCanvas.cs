using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuCanvas : MonoBehaviour
{
    public void Restart()
    {
        Scene scene = GameManager.Instance.SceneManager.GetActiveSecene();
        StartCoroutine(GameManager.Instance.LoadScene(scene.buildIndex, false));
    }

    public void MoveToLevelSelection()
    {
        StartCoroutine(GameManager.Instance.LoadScene(2, false));
    }

    public void MoveToMainMenu()
    {
        StartCoroutine(GameManager.Instance.LoadScene(1, false));
    }
}
