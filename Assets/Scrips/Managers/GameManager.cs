#region

using System.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.SceneManagement;

#endregion

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private SceneHandler _sceneManager;
    [SerializeField] private PrsestanSceneManager _prsestanScene;
    [SerializeField] public PlayerManager playerManager;
 
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
        StartCoroutine(LoadManu());
    }
    
    public void LoadScene(int index)
    {
        StartCoroutine(LoadSceneCoroutine(index));
    }

    public IEnumerator LoadManu()
    {
        yield return StartCoroutine(_sceneManager.LoadSceneAsync(1));

        yield return StartCoroutine(_prsestanScene.FadeOut());

        StartCoroutine(_sceneManager.ActiveScene());
    }

    private IEnumerator LoadSceneCoroutine(int index)
    {
        yield return StartCoroutine(_prsestanScene.FadeIn());

        yield return StartCoroutine(_sceneManager.LoadSceneAsync(index));

        yield return StartCoroutine(_prsestanScene.FadeOut());

        StartCoroutine(_sceneManager.ActiveScene());
    }
}