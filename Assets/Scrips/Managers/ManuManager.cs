#region

using UnityEngine;

#endregion

public class ManuManager : MonoBehaviour
{
    public void LoadLevel(int index)
    {
        StartCoroutine(GameManager.Instance.LoadScene(index,false));
    }
}