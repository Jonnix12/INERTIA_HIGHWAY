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
    [SerializeField] private Transform SpawnPoint;
    

    private CarController carController;
    

    #endregion
    private void Awake()
    {
        carController = GetComponent<CarController>();
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

    //private void TrackCheckpoints_OnCarWrongCheckpoint(object sender, CarCheckPointHalper e) 
    //{ 
    //    if(e.carTrasform == transform)
    //    {
    //        AddReward(-1f);
    //    }
    //}
    //private void TrackCheckpoints_OnCarCorrectCheckpoint(object sender, CarCheckPointHalper e)
    //{
    //    if (e.carTrasform == transform)
    //    {
    //        AddReward(+1f);
    //    }
    //}

    #region MLAgentActions
    public override void OnEpisodeBegin()
    {
        transform.position = SpawnPoint.position + new Vector3(Random.Range(3.6f, -2.6f), 0, Random.Range(2f, -2f));
        transform.forward = SpawnPoint.forward;
       // checkPointHalper.SetNextCheckPoint(transform);
        carController.StopAllCoroutines();
        
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        Vector3 checkpointForward = checkPointHalper.NextCheckPoint.transform.forward;
        float directionDot = Vector3.Dot(transform.forward, checkpointForward);
        sensor.AddObservation(directionDot);
    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        float forwardAmount = 0f;
        float turnAmount = 0f;
        bool breakAmput = false;

        switch (actions.DiscreteActions[0])
        {
            case 0: forwardAmount = 0f; break;
            case 1: forwardAmount = +1f; break;
            case 2: forwardAmount = -1f; break;
        }
        switch (actions.DiscreteActions[1])
        {
            case 0: turnAmount = 0f; break;
            case 1: turnAmount = +1f; break;
            case 2: turnAmount = -1f; break;
        }

        carController.UpdateCarInputs(forwardAmount, turnAmount,breakAmput);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        int forwardAction = 0;
        if (Input.GetKey(KeyCode.UpArrow)) forwardAction = 1;
        if (Input.GetKey(KeyCode.DownArrow)) forwardAction = 2;

        int turnAction = 0;
        if (Input.GetKey(KeyCode.RightArrow)) turnAction = 1;
        if (Input.GetKey(KeyCode.LeftArrow)) turnAction = 2;

        ActionSegment<int> discreteActions = actionsOut.DiscreteActions;
        discreteActions[0] = forwardAction;
        discreteActions[1] = turnAction;
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
    #endregion
}
