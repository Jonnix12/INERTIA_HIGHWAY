using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct WheelStract
{
    #region Fields

    public WheelCollider Collider;
    public Transform Transform;
    
    private string _wheelName;
    private int _wheelPosition;

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

    public string WheelName
    {
        get { return _wheelName; }
    }

    public int WheelPosition
    {
        get { return _wheelPosition; }
    }

    #endregion

    #region PublicFuncation

    public void InhitWheel(int wheelPosition)
    {
        _wheelName = Transform.name;
        _wheelPosition = wheelPosition;
    }

    public void UpdateWheel()
    {
        WheelHit wheelHit;

        _wheelMotorTorque = Collider.motorTorque;
        _wheelBreakTorque = Collider.brakeTorque;
        Collider.GetGroundHit(out wheelHit);
        _wheelForwardSlip = wheelHit.forwardSlip;
        _wheelSidewaysSlip = wheelHit.sidewaysSlip;
        
        UpdateWheelVisal();
    }

    #endregion

    #region PrivateFuncation

    private void UpdateWheelVisal()
    {
        Vector3 pos;
        Quaternion rot;
        
        Collider.GetWorldPose(out pos,out rot);
        Transform.position = pos;
        Transform.rotation = rot;
        
        
    }

    #endregion
   
}
