#region

using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

#endregion

public class SceneHandler : MonoBehaviour
{
    private int _currentScene;

    private void Awake()
    {
        _currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
    }

    public async void LoadSceneAsync(int index, bool toUnload)
    {
        if (toUnload)
        {
            Scene preventsScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
            UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(preventsScene);
        }

        AsyncOperation nextScene =
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(index, LoadSceneMode.Additive);

        Scene nextSceneRef = UnityEngine.SceneManagement.SceneManager.GetSceneByBuildIndex(index);

        nextScene.allowSceneActivation = false;


        await Task.Delay(100);
        StartCoroutine(WaitForSceneToLoad(nextScene, nextSceneRef));
    }

    public async void LoadNextScene()
    {
        _currentScene++;

        AsyncOperation scene = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(_currentScene);

        scene.allowSceneActivation = false;

        if (scene.isDone)
        {
            await Task.Delay(500);
        }
    }

    private IEnumerator WaitForSceneToLoad(AsyncOperation scene, Scene sceneRef)
    {
        while (scene.progress < 0.9f)
        {
            yield return null;
        }

        scene.allowSceneActivation = true;
        yield return new WaitForSeconds(1);
        UnityEngine.SceneManagement.SceneManager.SetActiveScene(sceneRef);
    }

    public Scene GetActiveSecene()
    {
        return SceneManager.GetActiveScene();
    }
}