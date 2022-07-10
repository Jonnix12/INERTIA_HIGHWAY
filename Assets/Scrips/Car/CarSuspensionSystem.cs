#region

using UnityEngine;

#endregion

public class CarSuspensionSystem : MonoBehaviour
{
    [Header("Wheels")] [SerializeField] private Wheel[] _wheels;
    public float wheelRadius;
    [SerializeField] private Rigidbody _rb;

    [Header("Suspension")] public float restLength;
    public float springTravel;
    public float springStiffness;
    public float damperStiffness;
    [SerializeField] private float _wheelRotation;

    [Header("Brakes")] [SerializeField] private float _brakeForce;

    public Wheel[] Wheels => _wheels;

    protected void InitSuspension()
    {
        for (int i = 0; i < Wheels.Length; i++)
        {
            _wheels[i].InhitWheel(_rb, wheelRadius, restLength, springTravel, springStiffness, damperStiffness,
                _brakeForce, _wheelRotation);
        }
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < Wheels.Length; i++)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(Wheels[i].transform.position,
                Wheels[i].transform.position + Wheels[i].transform.up * -_wheels[i].SpringLength);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(Wheels[i].transform.position + Wheels[i].transform.up * -_wheels[i].SpringLength,
                wheelRadius);
        }
    }
}