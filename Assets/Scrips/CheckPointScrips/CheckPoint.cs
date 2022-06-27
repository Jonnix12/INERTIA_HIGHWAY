using System;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public event Action<CarCheckPointHalper,CheckPoint> OnCheckPointTrigger;

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
