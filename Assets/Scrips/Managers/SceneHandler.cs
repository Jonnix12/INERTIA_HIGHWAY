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
        _presistanScene = SceneManager.GetActiveScene();
    }

    public AsyncOperation LoadSceneAsync(int index, bool isAdditive)
    {
        _isSceneLoaded = false;

        Scene preventsScene = SceneManager.GetActiveScene();
        
        SceneManager.SetActiveScene(_presistanScene);
        
        SceneManager.UnloadSceneAsync(preventsScene);
        
        _scene = SceneManager.LoadSceneAsync(index,LoadSceneMode.Additive);
        //_scene = SceneManager.LoadSceneAsync(index,isAdditive? LoadSceneMode.Additive : LoadSceneMode.Single);
        _sceneRef = SceneManager.GetSceneByBuildIndex(index);

        _scene.allowSceneActivation = false;

        return _scene;
    }
    
    public AsyncOperation LoadSceneAsyncFirstScene(int index, bool isAdditive)
    {
        _isSceneLoaded = false;
        
        Scene preventsScene = SceneManager.GetActiveScene();
        SceneManager.UnloadScene(preventsScene);
        
        _scene = SceneManager.LoadSceneAsync(index,isAdditive? LoadSceneMode.Additive : LoadSceneMode.Single);
        
        _sceneRef = SceneManager.GetSceneByBuildIndex(index);

        _scene.allowSceneActivation = false;

        return _scene;
    }

    public async void LoadNextScene()
    {
        _currentScene++;

        AsyncOperation scene = SceneManager.LoadSceneAsync(_currentScene);

        scene.allowSceneActivation = false;

        if (scene.isDone)
        {
            await Task.Delay(500);
        }
    }
    
    public void ActiveCurrentSecne()
    {
        SceneManager.SetActiveScene(_sceneRef);
    }

    public Scene GetActiveSecene()
    {
        return SceneManager.GetActiveScene();
    }
}