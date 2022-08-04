using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class PlayerPrefManager 
{
    static public bool IsFirstTime()
    {
        if (PlayerPrefs.GetInt("isFirstTime") == 0)
        {
            PlayerPrefs.SetInt("isFirstTime", 1);
            return true;
        }

        return false;
    }

    static public void ResetPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}
