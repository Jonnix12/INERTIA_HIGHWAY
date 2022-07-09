using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   public static GameManager Instance;
   [SerializeField] private SceneManager _sceneManager;
   [SerializeField] private PrsestanSceneManager _prsestanScene;

   public SceneManager SceneManager
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
      //AsyncOperation manuScene = SceneManager.LoadManu();
      
      _prsestanScene.FadeViewPort(false);
      
      while (_prsestanScene.IsFadeIn)
      {
         yield return null;
      }

     // manuScene.allowSceneActivation = true;
   }
}
