#region

using UnityEngine;

#endregion

public class ManuManager : MonoBehaviour
{
    public void LoadLevelSelection(int index)
    {
        StartCoroutine(GameManager.Instance.LoadScene(index));
    }
}