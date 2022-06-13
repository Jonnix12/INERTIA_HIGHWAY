using UnityEngine;

public class CarSteeringSystem : MonoBehaviour
{
    #region Fields

    [Header("Wheels")] 
    [SerializeField] private WheelStruct[] _wheels;

    [Header("Steering System Parameters")] 
    [SerializeField] private float _maxSteerAngel;

    [SerializeField] private float _inputDeadZone;

    [Header("Steering Anchors Transform")] 
    [SerializeField] private Transform _leftAnchor;

    [SerializeField] private Transform _rightAnchor;


    private float _steerInput;

    private Vector3 _leftAnchorBasePosition;
    private Vector3 _rightAnchorBasePosition;
    private Vector3 _leftAnchorTarget;
    private Vector3 _rightAnchorTarget;

    private Vector3 _leftDefaultWheelSteerDir;
    private Vector3 _rightDefaultWheelSteerDir;

    private float _leftInput;
    private float _rightInput;

    private bool _isTruningLeft;
    private bool _isTruningRight;

    #endregion
    
    #region Prop

    public WheelStruct[] Wheels => _wheels;

    #endregion

    #region UnityCallback

    private void Awake()
    {
        WheelHalper.onSetDir += GetWheelDirVector;
    }

    #endregion
    
    #region ProtectedFunction

    protected void InitSteeringSystem()
    {
        _leftAnchorBasePosition = _leftAnchor.localPosition;
        _rightAnchorBasePosition = _rightAnchor.localPosition;
        _leftAnchorTarget = new Vector3(_leftAnchor.position.x, 0, -_maxSteerAngel);
        _rightAnchorTarget = new Vector3(_rightAnchor.position.x, 0, -_maxSteerAngel);

        for (var i = 0; i < _wheels.Length; i++) _wheels[i].InhitWheel(i);
    }

    protected void UpdateSteeringSystem()
    {
        
    }

    protected void GetSteeringInput(float input)
    {
        if (input < -_inputDeadZone)
        {
            _leftInput = Mathf.Abs(input);
            _isTruningLeft = true;
        }
        else if (input > _inputDeadZone)
        {
            _rightInput = input;
            _isTruningRight = true;
        }
        else
        {
            _rightInput = 0;
            _leftInput = 0;
            _isTruningLeft = false;
            _isTruningRight = false;
        }
    }

    protected void UpdateWheelSteerAngel()
    {
        MoveAnchorPosition();

        if (_isTruningLeft)
        {
            _wheels[0].Collider.steerAngle = -CalculateSteerAngel(_leftAnchor.position, 0);
            _wheels[1].Collider.steerAngle = -180 + CalculateSteerAngel(_leftAnchor.position, 1);
        }
        else if (_isTruningRight)
        {
            _wheels[0].Collider.steerAngle = 180 - CalculateSteerAngel(_rightAnchor.position, 0);
            _wheels[1].Collider.steerAngle = CalculateSteerAngel(_rightAnchor.position, 1);
        }
        else
        {
            _wheels[0].Collider.steerAngle = 0;
            _wheels[1].Collider.steerAngle = 0;
        }
    }

    #endregion

    #region PrivateFunctions

    private void MoveAnchorPosition()
    {
        _leftAnchor.localPosition = Vector3.Lerp(_leftAnchorBasePosition, _leftAnchorTarget, _leftInput);
        _rightAnchor.localPosition = Vector3.Lerp(_rightAnchorBasePosition, _rightAnchorTarget, _rightInput);
    }

    private float CalculateSteerAngel(Vector3 target, int index)
    {
        var dir = _wheels[index].WheelPosition.InverseTransformPoint(target) - Vector3.zero;

        var angel = Vector3.Angle(_wheels[index].DefaultWheelSteerDir, dir.normalized);

        return angel;
    }

    private void GetWheelDirVector(Vector3 dir, int index, Transform position)
    {
        _wheels[index].DefaultWheelSteerDir = dir;
        _wheels[index].WheelPosition = position;
    }

    #region Gizmos

    private void OnDrawGizmosSelected()
    {
        for (var i = 0; i < _wheels.Length; i++)
        {
            Gizmos.color = Color.blue;
            //var ray = new Ray(_wheels[i].WheelPosition.position, _wheels[i].DefaultWheelSteerDir);
           // Gizmos.DrawRay(ray);
        }

        for (var i = 0; i < _wheels.Length / 2; i++)
        {
            Gizmos.color = Color.red;
            var rayLeft = new Ray(_wheels[i].Collider.transform.position, _wheels[i].WheelSidewaysDirLeft);
            Gizmos.DrawRay(rayLeft);
            var rayRight = new Ray(_wheels[i].Collider.transform.position, _wheels[i].WheelSidewaysDirRight);
            Gizmos.DrawRay(rayRight);
        }

        Gizmos.color = Color.green;
        Gizmos.DrawSphere(_leftAnchor.position, 0.5f);
        Gizmos.DrawSphere(_rightAnchor.position, 0.5f);
    }

    #endregion

    #endregion
}