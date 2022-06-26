using System;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public event Action<CarCheckPointHalper,CheckPoint> OnCheckPointTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            CarCheckPointHalper carTemp;
            
            if (other.TryGetComponent<CarCheckPointHalper>(out carTemp))
            {
                OnCheckPointTrigger?.Invoke(carTemp,this);
            }
        }
    }
}
