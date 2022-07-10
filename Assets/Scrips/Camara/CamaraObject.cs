#region

using UnityEngine;

#endregion

public class CamaraObject : MonoBehaviour
{
    [Header("Camera LookAT")] [SerializeField]
    private Transform _cameraLookAT;

    void Start()
    {
        CamaraFallowCar.SetTarget(_cameraLookAT);
    }
}