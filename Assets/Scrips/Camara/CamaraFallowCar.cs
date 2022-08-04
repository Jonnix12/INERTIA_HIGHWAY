#region

using UnityEngine;

#endregion

public class CamaraFallowCar : MonoBehaviour
{
    public static bool IsLookBackInput = false;

    [SerializeField] private static Transform _target;
    [SerializeField] private Vector3 _offSet;
    [SerializeField] private float _timeToSmoothDamp = 1f;

    private Vector3 _velocity = Vector3.zero;
    

    void FixedUpdate()
    {
        Vector3 tragetPosition = _target.position + (_target.rotation * _offSet);
        Vector3 camPosition = transform.position;
        
        Vector3 tragetRotation = _target.rotation.eulerAngles;
        Vector3 camRotation = transform.rotation.eulerAngles;
        
        transform.position =
            Vector3.SmoothDamp(camPosition, tragetPosition, ref _velocity, _timeToSmoothDamp * Time.deltaTime);
        
        //transform.rotation = Quaternion.Euler(Vector3.SmoothDamp(camRotation, tragetRotation, ref _velocity, _timeToSmoothDamp * Time.deltaTime));


        transform.LookAt(_target, Vector3.up);

        ChanageLook();
    }

    private void ChanageLook()
    {
        _offSet.z = IsLookBackInput ? 6 : -6; //need Work
    }

    public static void SetTarget(Transform target)
    {
        _target = target;
    }
}