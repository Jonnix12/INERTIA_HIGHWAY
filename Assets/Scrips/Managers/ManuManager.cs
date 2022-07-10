#region

using UnityEngine;

#endregion

public class ManuManager : MonoBehaviour
{
    public void LoadRace(int index)
    {
        StartCoroutine(GameManager.Instance.LoadScene(index));
    }
}