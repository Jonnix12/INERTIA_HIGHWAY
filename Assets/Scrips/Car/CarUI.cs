#region

using TMPro;
using UnityEngine;

#endregion

public class CarUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI speed;
    [SerializeField] private TextMeshProUGUI enginRPM;
    [SerializeField] private TextMeshProUGUI gear;

    [SerializeField] private CarController _car;

    // Update is called once per frame
    void Update()
    {
        //speed.text = _car.CarSpeed.ToString();
        enginRPM.text = _car.EngineRpm.ToString();
        //gear.text = _car.Gear.ToString();
    }
}