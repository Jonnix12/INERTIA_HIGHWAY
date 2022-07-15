#region

using UnityEngine;

#endregion

public class ManuManager : MonoBehaviour
{
    public void LoadLevel(int index)
    {
        GameManager.Instance.LoadScene(index);
    }

    public void Exit()
    {
        Application.Quit();
    }
}