using System;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public event Action<CarCheckPointHalper,CheckPoint> OnCheckPointTrigger;
    private int _id = 0;

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
            if (other.TryGetComponent<CarCheckPointHalper>(out CarCheckPointHalper carTemp))
            {
                OnCheckPointTrigger?.Invoke(carTemp,this);
            }
        }
    }
}
