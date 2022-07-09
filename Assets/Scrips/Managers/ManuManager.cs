using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManuManager : MonoBehaviour
{
   public void LoadRace1()
   {
      GameManager.Instance.SceneManager.LoadSceneAsync(2);
   }
}
