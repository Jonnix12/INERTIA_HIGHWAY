using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public struct WheelStract
{
    #region Fields

    public WheelCollider Collider;
    
    [HideInInspector] public Vector3 DefaultWheelSteerDir;
    [HideInInspector] public Transform WheelPosition;
    
    [SerializeField] private Transform meshTransform;

    private Vector3 _wheelForwardDir;
    private Vector3 _wheelSidewaysDirLeft;
    private Vector3 _wheelSidewaysDirRight;
    
    private string _wheelName;
    private int _wheelIndex;
    
    private float _wheelMotorTorque;
    private float _wheelBreakTorque;
    private float _wheelForwardSlip;
    private float _wheelSidewaysSlip;
    

    #endregion
    
    #region Porp

    public float MotorTorque
    {
        get { return _wheelMotorTorque; }
    }
    
    public float BreakTorque
    {
        get { return _wheelBreakTorque; }
    }
    
    public float FowardSlip
    {
        get { return _wheelForwardSlip; }
    }
    
    public float SideSlip
    {
        get { return _wheelSidewaysSlip; }
    }
    
    public Vector3 WheelForwardDir
    {
        get { return _wheelForwardDir; }
    }

    public Vector3 WheelSidewaysDirLeft
    {
        get { return _wheelSidewaysDirLeft; }
    }

    public Vector3 WheelSidewaysDirRight
    {
        get { return _wheelSidewaysDirRight; }
    }

    public string WheelName
    {
        get { return _wheelName; }
    }

    public int WheelIndex
    {
        get { return _wheelIndex; }
    }

    #endregion

    #region PublicFuncation

    public void InhitWheel(int wheelPosition)
    {
        _wheelName = meshTransform.name;
        _wheelIndex = wheelPosition;
    }

    public void UpdateWheel()
    {
        WheelHit wheelHit;

        _wheelMotorTorque = Collider.motorTorque;
        _wheelBreakTorque = Collider.brakeTorque;
        
        Collider.GetGroundHit(out wheelHit);
        _wheelForwardSlip = wheelHit.forwardSlip;
        _wheelSidewaysSlip = wheelHit.sidewaysSlip;
        _wheelForwardDir = wheelHit.forwardDir;
        _wheelSidewaysDirLeft = wheelHit.sidewaysDir;
        _wheelSidewaysDirRight = -wheelHit.sidewaysDir;

        UpdateWheelVisal();
    }

    #endregion
    
    #region PrivateFuncation

    private void UpdateWheelVisal()
    {
        Vector3 pos;
        Quaternion rot;
        
        Collider.GetWorldPose(out pos,out rot);
        meshTransform.position = pos;
        meshTransform.rotation = rot;
    }

    #endregion
   
}
