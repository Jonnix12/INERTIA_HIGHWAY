#region

using System.Collections;
using UnityEngine;

#endregion

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public SceneHandler _sceneManager;
    public PrsestanSceneManager _prsestanScene;

    public SceneHandler SceneManager
    {
        get { return _sceneManager; }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        StartCoroutine(LoadSceneManu());
    }

    private IEnumerator LoadSceneManu()
    {
        SceneManager.LoadSceneAsync(1, false);

        _prsestanScene.FadeViewPort(false);

        while (_prsestanScene.IsFadeIn)
        {
            yield return null;
        }
    }

    public IEnumerator LoadScene(int index)
    {
        _prsestanScene.FadeViewPort(true);

        while (!_prsestanScene.IsFadeIn)
        {
            yield return null;
        }

        SceneManager.LoadSceneAsync(index, true);

        _prsestanScene.FadeViewPort(false);

        while (_prsestanScene.IsFadeIn)
        {
            yield return null;
        }
    }
}