using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CarUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI speed;
    [SerializeField] private TextMeshProUGUI enginRPM;

    [SerializeField] private CarController _car;

    // Update is called once per frame
    void Update()
    {
        speed.text = _car.CarSpeed.ToString();
        enginRPM.text = _car.EngineRpm.ToString();
    }
}
