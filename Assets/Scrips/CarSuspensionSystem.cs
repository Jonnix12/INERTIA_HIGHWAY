using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSuspensionSystem : MonoBehaviour {

    [Header("Wheels")] 
    [SerializeField] private Wheel[] _wheels;
    [SerializeField] private Rigidbody rb;
    
    [Header("Suspension")]
    public float restLength;
    public float springTravel;
    public float springStiffness;
    public float damperStiffness;
    
    [Header("Wheel")]
    public float wheelRadius;
    
    public Wheel[] Wheels => _wheels;
    
    void Start() 
    {
        for (int i = 0; i < Wheels.Length; i++)
        {
            _wheels[i].InhitWheel(rb,wheelRadius,restLength,springTravel,springStiffness,damperStiffness);
        }
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < Wheels.Length; i++)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(Wheels[i].transform.position, Wheels[i].transform.position + Wheels[i].transform.up * -_wheels[i].SpringLength);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(Wheels[i].transform.position + Wheels[i].transform.up * -_wheels[i].SpringLength, wheelRadius);   
        }
    }

    // protected void UpdateWheelSuspension()
    // {
    //     for (int i = 0; i < Wheels.Length; i++)
    //     {
    //         if (Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, _wheels[i].MaxLength + wheelRadius)) {
    //
    //             _wheels[i].LastLength = _wheels[i].SpringLength;
    //         
    //             _wheels[i].SpringLength = hit.distance - wheelRadius;
    //             _wheels[i].SpringLength = Mathf.Clamp(_wheels[i].SpringLength, _wheels[i].MaxLength, _wheels[i].MaxLength);
    //             _wheels[i].SpringVelocity = (_wheels[i].LastLength - _wheels[i].SpringLength) / Time.fixedDeltaTime;
    //             _wheels[i].SpringForce = springStiffness * (restLength - _wheels[i].SpringLength);
    //             _wheels[i].DamperForce = damperStiffness * _wheels[i].SpringVelocity;
    //         
    //             suspensionForce = (_wheels[i].SpringForce + _wheels[i].DamperForce) * transform.up;
    //         
    //             rb.AddForceAtPosition(suspensionForce, hit.point);
    //         }
    //     }
    // }
}