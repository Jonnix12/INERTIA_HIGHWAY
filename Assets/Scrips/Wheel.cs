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
    private float _springStiffness;
    private float _damperStiffness;

    private RaycastHit _hit;
    
    private Vector3 _wheelPosition;
    private Vector3 _wheelPositionOffSet;
    private Vector3 _wheelVelocity;
    
    private float _wheelRadius;
    private float _desirableWheelForce;
    private float _wheelForceZ;
    private float _wheelForceX;
    
    private Rigidbody _rb;

    private float tempInput;

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
        _springStiffness = springStiffness;
        _damperStiffness = damperStiffness;
        
        _minLength = restLength - springTravel;
        _maxLength = restLength + springTravel;

        _wheelPositionOffSet = new Vector3(0, wheelRadius, 0);
    }
    
    public void SetWheelAngel(float angel)
    {
        transform.localRotation = Quaternion.Euler(transform.rotation.x,transform.rotation.y + angel,transform.rotation.z);
    }

    public void AddWheelForce(float force)
    {
        tempInput = force;
    }

    public void Braek()
    {
        
    }

    #region UnityCallBack

    

    #endregion
    private void FixedUpdate()
    {
        if (Physics.Raycast(transform.position, -transform.up, out _hit, _maxLength + _wheelRadius)) {
            
            _rb.AddForceAtPosition(CalculateSuspensionForce() + CalculateForceOnWheel(), _hit.point);
            _wheelPosition = new Vector3(0, -(_springLength), 0);
        }
    }

    
    private void Update()
    {
        UpdateWheelVisal();
    }

    #endregion
    
    #region PrivateFuncation

    private void UpdateWheelVisal()
    {
        meshTransform.localPosition = _wheelPosition;
    }
    
    private Vector3 CalculateSuspensionForce()
    {
        _lastLength = _springLength;
            
        _springLength = _hit.distance - _wheelRadius;
        _springLength = Mathf.Clamp(_springLength, _minLength, _maxLength);
        _springVelocity = (_lastLength - _springLength) / Time.fixedDeltaTime;
        _springForce = _springStiffness * (_restLength - _springLength);
        _damperForce = _damperStiffness * _springVelocity;

        return (_springForce + _damperForce) * transform.up;
    }

    private Vector3 CalculateForceOnWheel()
    {
        _wheelVelocity = transform.InverseTransformDirection(_rb.GetPointVelocity(_hit.point));
        _wheelForceZ = tempInput * 2000 * 0.5f;
        _wheelForceX = _wheelVelocity.x * 3000;

        return _wheelForceZ * transform.forward + _wheelForceX * -transform.right;
    }


    #endregion
   
}
