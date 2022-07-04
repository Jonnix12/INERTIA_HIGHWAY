using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class CarMoveAgent : Agent
{
    #region fields
    [SerializeField] private CarCheckPointHalper checkPointHalper;
    [SerializeField] private Vector3 SpawnPoint;
    

    private CarController carController;
    

    #endregion
    
    private void Awake()
    {
        carController = GetComponent<CarController>();
        SpawnPoint = transform.position;
    }

    private void Start()
    {
        checkPointHalper.OnPassCheckPoint += AddRewardCurrntly;
        checkPointHalper.PassWrongCheckPass += RemoveReward;
    }

    private void AddRewardCurrntly()
    {
        AddReward(+1f);
    }

    private void RemoveReward()
    {
        AddReward(-1f);
    }

    private void SpeedReword()
    {
        //AddReward(carController.CarSpeed * 0.1f);
    }
    
    #region MLAgentActions
    public override void OnEpisodeBegin()
    {
        transform.position = SpawnPoint;
        transform.rotation = new Quaternion(0, 0, 0, 0);
    }
    
    public override void CollectObservations(VectorSensor sensor)
    {
        Vector3 checkpointForward = checkPointHalper.NextCheckPoint.transform.forward;
        float directionDot = Vector3.Dot(transform.forward, checkpointForward);
        Debug.Log(directionDot);
        sensor.AddObservation(directionDot);
        //sensor.AddObservation(checkpointForward);
    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        float forwardAmount = actions.ContinuousActions[0];
        float turnAmount = actions.ContinuousActions[1];
        bool breakAmput = false;

        if (forwardAmount < 0)
        {
            AddReward(-0.1f);
        }

        // if (forwardAmount > 0)
        // {
        //     AddReward(0.1f);
        // }
        
        carController.UpdateCarInputs(forwardAmount, turnAmount,breakAmput);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        int forwardAction = 0;
        if (Input.GetKey(KeyCode.UpArrow)) forwardAction = 1;
        if (Input.GetKey(KeyCode.DownArrow)) forwardAction = -1;

        int turnAction = 0;
        if (Input.GetKey(KeyCode.RightArrow)) turnAction = 1;
        if (Input.GetKey(KeyCode.LeftArrow)) turnAction = -1;

        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = forwardAction;
        continuousActions[1] = turnAction;
    }
    #endregion
    
    #region collison
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            AddReward(-0.5f);
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            AddReward(-0.1f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("End"))
        {
            EndEpisode();
        }
    }

    #endregion
}
