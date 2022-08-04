#region

using UnityEngine;

#endregion

public class SpeedMotor : MonoBehaviour
{
    public CarController car;


    public float maxSpeed;
    public float minSpeedArrowAngle;
    public float maxSpeedArrowAngle;

    [Header("UI")] public RectTransform arrow;

    private float speed;

    private void Update()
    {
        speed = car.speed;
        if (arrow != null)
            arrow.localEulerAngles =
                new Vector3(0, 0, Mathf.Lerp(minSpeedArrowAngle, maxSpeedArrowAngle, speed / maxSpeed));
    }
}