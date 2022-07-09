using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManuManager : MonoBehaviour
{
    public void LoadRace(int index)
   {
       StartCoroutine(GameManager.Instance.LoadScene(index));
   }
}
