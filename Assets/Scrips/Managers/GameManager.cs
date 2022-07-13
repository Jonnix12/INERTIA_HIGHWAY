#region

using System.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

#endregion

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private SceneHandler _sceneManager;
    [SerializeField] private PrsestanSceneManager _prsestanScene;
 
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
        StartCoroutine(LaodManu());
    }

    public IEnumerator LaodManu()
    {
        _prsestanScene.FadeViewPort(true);
        
        yield return new WaitUntil(() => _prsestanScene.IsFadeIn);

        AsyncOperation scene = _sceneManager.LoadSceneAsyncFirstScene(1, true);

        
        yield return new WaitUntil(() => scene.progress > 0.85f);

        scene.allowSceneActivation = true;
        
        _prsestanScene.FadeViewPort(false);
        
        yield return new WaitUntil(() => !_prsestanScene.IsFadeIn);
        
        _sceneManager.ActiveCurrentSecne();
    }

    public IEnumerator LoadScene(int index,bool isAdditive)
    {
        _prsestanScene.FadeViewPort(true);
        
        yield return new WaitUntil(() => _prsestanScene.IsFadeIn);
        
        AsyncOperation scene = _sceneManager.LoadSceneAsync(index, isAdditive);

        //yield return new WaitUntil(() => (scene.progress > 0.85f));

        while (scene.progress < 0.85f)
        {
            yield return new WaitForEndOfFrame();
        }

        scene.allowSceneActivation = true;
        
        _prsestanScene.FadeViewPort(false);
        
        yield return new WaitUntil(() => !_prsestanScene.IsFadeIn);
        
        _sceneManager.ActiveCurrentSecne();
    }
}