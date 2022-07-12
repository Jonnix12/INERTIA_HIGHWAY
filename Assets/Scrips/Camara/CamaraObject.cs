#region

using UnityEngine;

#endregion

public class CamaraObject : MonoBehaviour
{
    [Header("Camera LookAT")] [SerializeField]
    private Transform _cameraLookAT;

    void Awake()
    {
        CamaraFallowCar.SetTarget(_cameraLookAT);
    }
}