using System;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public event Action<GameObject,int> OnCheckPoineTrigger;
    private int _id;

    public int ID
    {
        get { return _id; }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car"))
        {
           OnCheckPoineTrigger?.Invoke(other.gameObject,ID);
        }
    }

    public void SetID(int id)
    {
        if (_id != 0)
            return;
        
        _id = id;
    }
}
