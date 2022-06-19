using System;
using UnityEngine;


public class Wheel : MonoBehaviour
{
    #region Fields

    [SerializeField] private Transform meshTransform;
    
    private float _springLength;
    private float _springForce;
    private float _damperForce;
    private float _springVelocity;
    
    private float _lastLength;
    private float _minLength;
    private float _maxLength;
    
    private float _restLength;
    private float _springTravel;
    private float _springStiffness;
    private float _damperStiffness;

    private Vector3 _wheelPosition;
    private Vector3 _wheelVelocity;
    private float _wheelRadius;
    private float _desirableWheelForce;
    private float _wheelForceZ;
    private float _wheelForceX;
    
    //private float steerAngle;
    
    private Vector3 _suspensionForce;

    private Rigidbody _rb;

    #endregion

    public float SpringLength
    {
        get { return _springLength; }
    }
    
    #region PublicFuncation

    public void InhitWheel(Rigidbody rb, float wheelRadius,float restLength,float springTravel,float springStiffness , float damperStiffness)
    {
        _wheelRadius = wheelRadius;
        _rb = rb;
        _restLength = restLength;
        _springTravel = springTravel;
        _springStiffness = springStiffness;
        _damperStiffness = damperStiffness;
        
        _minLength = restLength - springTravel;
        _maxLength = restLength + springTravel;
    }
    
    public void SetWheelAngel(float angel)
    {
        transform.localRotation = Quaternion.Euler(transform.rotation.x,transform.rotation.y + angel,transform.rotation.z);
    }

    public void AddWheelForce(float force)
    {
        Debug.Log(force);
        _desirableWheelForce += force;
    }
    
    private void FixedUpdate()
    {
        if (Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, _maxLength + _wheelRadius)) {

            _lastLength = _springLength;
            
            _springLength = hit.distance - _wheelRadius;
            _springLength = Mathf.Clamp(_springLength, _minLength, _maxLength);
            _springVelocity = (_lastLength - _springLength) / Time.fixedDeltaTime;
            _springForce = _springStiffness * (_restLength - _springLength);
            _damperForce = _damperStiffness * _springVelocity;
            
            _suspensionForce = (_springForce + _damperForce) * transform.up;

            _wheelVelocity = transform.InverseTransformDirection(_rb.GetPointVelocity(hit.point));
            _wheelForceZ = _desirableWheelForce;
            _wheelForceX = _wheelVelocity.x * _desirableWheelForce;
            
            _rb.AddForceAtPosition(_suspensionForce + _wheelForceZ * transform.forward + _wheelForceX * -transform.right , hit.point);
        }
        
    }

    private void Update()
    {
        _wheelPosition = new Vector3(transform.position.x, _springLength - _wheelRadius / 2, transform.position.z);
        UpdateWheelVisal();
    }

    #endregion
    
    #region PrivateFuncation

    private void UpdateWheelVisal()
    {
        meshTransform.position = _wheelPosition;
    }

    #endregion
   
}
