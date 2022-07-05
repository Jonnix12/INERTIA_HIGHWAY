using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedMotor : MonoBehaviour
{
    public CarController car;
    

    public float maxSpeed = 0.0f; 
    public float minSpeedArrowAngle;
    public float maxSpeedArrowAngle;

    [Header("UI")]
   
    public RectTransform arrow; 

    private float speed = 0.0f;
    private void Update()
    {

        speed = car.speed;
        if (arrow != null)
            arrow.localEulerAngles =
                new Vector3(0, 0, Mathf.Lerp(minSpeedArrowAngle, maxSpeedArrowAngle, speed / maxSpeed));
    }
}

