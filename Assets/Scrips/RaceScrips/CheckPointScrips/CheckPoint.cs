#region

using System;
using UnityEngine;

#endregion

public class CheckPoint : MonoBehaviour
{
    public event Action<CarCheckPointHelper, CheckPoint> OnCheckPointTrigger;
    private int _id;

    public int ID
    {
        get { return _id; }
    }

    public void SetId(int id)
    {
        _id = id;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            if (other.TryGetComponent(out CarCheckPointHelper carTemp))
            {
                OnCheckPointTrigger?.Invoke(carTemp, this);
            }
        }
    }
}