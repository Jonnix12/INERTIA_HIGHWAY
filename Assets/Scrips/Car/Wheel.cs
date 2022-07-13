#region

using UnityEngine;

#endregion

public class Wheel : MonoBehaviour
{
    #region Fields

    [SerializeField] private Transform meshTransform;

    private float _brakeForce;
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

    private readonly int _forceMultiplierZ = 1000;
    private readonly int _forceMultiplierX = 2000;

    private RaycastHit _hit;

    private Vector3 _wheelPosition;
    private Vector3 _wheelVelocity;

    private float _wheelRadius;
    private float _desirableWheelForce;
    private float _wheelForceZ;
    private float _wheelForceX;
    private float _wheelRotation;

    private Rigidbody _rb;

    private float tempInput;

    #endregion

    public float SpringLength
    {
        get { return _springLength; }
    }

    #region PublicFuncation

    public void InitWheel(Rigidbody rb, float wheelRadius, float restLength, float springTravel, float springStiffness,
        float damperStiffness, float brakeForce, float wheelRotation)
    {
        _wheelRadius = wheelRadius;
        _rb = rb;
        _restLength = restLength;
        _springStiffness = springStiffness;
        _damperStiffness = damperStiffness;
        _brakeForce = brakeForce;
        _wheelRotation = wheelRotation;

        _minLength = restLength - springTravel;
        _maxLength = restLength + springTravel;
    }

    public void SetWheelAngel(float angel)
    {
        transform.localRotation =
            Quaternion.Euler(transform.rotation.x, transform.rotation.y + angel, transform.rotation.z);
    }

    public void AddWheelForce(float force, float SpeedStopMultiplier)
    {
        if (Physics.Raycast(transform.position, -transform.up, out _hit, _maxLength + _wheelRadius))
        {
            _rb.AddForceAtPosition(CalculateSuspensionForce() + CalculateForceOnWheel(force, SpeedStopMultiplier),
                _hit.point);
        }
    }

    public void Brake()
    {
        Vector3 refVector = Vector3.zero;
        Vector3.SmoothDamp(_rb.GetPointVelocity(_hit.point), Vector3.zero, ref refVector, 0.5f);

        _rb.AddForceAtPosition(refVector * _brakeForce, _hit.point);
    }

    #endregion

    #region PrivateFuncation

    public void UpdateWheelVisal()
    {
        _wheelPosition = new Vector3(0, -(_springLength), 0);
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

    private Vector3 CalculateForceOnWheel(float input, float SpeedStopMultiplier)
    {
        _wheelVelocity = transform.InverseTransformDirection(_rb.GetPointVelocity(_hit.point));
        _wheelForceZ = input * _forceMultiplierZ / SpeedStopMultiplier;
        _wheelForceX = _wheelVelocity.x * _springForce;
        return _wheelForceZ * transform.forward + _wheelForceX * -transform.right;
    }

    #endregion
}