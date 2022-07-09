using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.SceneManagement;
using Task = System.Threading.Tasks.Task;

public class SceneManager : MonoBehaviour
{
    private int _currentScene;
    
    private void Awake()
    {
        _currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
    }

    public async void LoadSceneAsync(int index)
    {
        AsyncOperation scene = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(index);

        scene.allowSceneActivation = false;

        if (scene.isDone)
        {
            await Task.Delay(500);
            scene.allowSceneActivation = true;
        }
    }

    public void LoadScene(int index)
    {
        UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(index - 1);
        UnityEngine.SceneManagement.SceneManager.LoadScene(index,LoadSceneMode.Additive);
    }

    public AsyncOperation LoadManu()
    {
       AsyncOperation scene = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(1,LoadSceneMode.Additive);
       scene.allowSceneActivation = false;

       return scene;
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

    
}
