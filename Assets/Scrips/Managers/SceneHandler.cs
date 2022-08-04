#region

using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

#endregion

public class SceneHandler : MonoBehaviour
{
    private int _currentScene;

    private bool _isSceneLoaded;

    private AsyncOperation _scene;
    private Scene _sceneRef;
    private Scene _presistanScene;

    private void Awake()
    {
        _presistanScene = SceneManager.GetSceneByBuildIndex(0);
    }

    public IEnumerator LoadSceneAsync(int index)
    {
        _isSceneLoaded = false;

        Scene preventsScene = SceneManager.GetActiveScene();
        
        SceneManager.SetActiveScene(_presistanScene);

        if (preventsScene.buildIndex != 0)
        {
             SceneManager.UnloadSceneAsync(preventsScene);
        }
        
        _scene = SceneManager.LoadSceneAsync(index,LoadSceneMode.Additive);
        
        _scene.allowSceneActivation = false;

        while (!_scene.isDone)
        {
            if (_scene.progress >= 0.89f)
            {
                _scene.allowSceneActivation = true;
                _sceneRef = SceneManager.GetSceneByBuildIndex(index);
                yield break;
            }
            
            _isSceneLoaded = _scene.isDone;
            yield return null;
        }
    }

    public IEnumerator ActiveScene()
    {
        while (!SceneManager.SetActiveScene(_sceneRef))
        {
            yield return null;
        }
        
    }
    
    public Scene GetActiveSecene()
    {
        return SceneManager.GetActiveScene();
    }
}