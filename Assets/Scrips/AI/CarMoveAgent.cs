#region

using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

#endregion

public class CarMoveAgent : Agent, Idisable
{
    #region fields

    [SerializeField] private CarCheckPointHelper checkPointHelper;
    [SerializeField] private Vector3 _spawnPoint;
    [SerializeField] private Vector3 _rotatePoint;
    private bool _isEnable;

    private CarController carController;

    #endregion

    private void Awake()
    {
        carController = GetComponent<CarController>();
        _spawnPoint = transform.position;
        _rotatePoint = transform.rotation.eulerAngles;
    }

    private void Start()
    {
        checkPointHelper.OnPassCheckPoint += AddRewardCurrntly;
        checkPointHelper.PassWrongCheckPass += RemoveReward; 
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
        transform.position = _spawnPoint;
        transform.rotation = Quaternion.Euler(_rotatePoint);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        if (checkPointHelper.NextCheckPoint == null)
            return;        
        
        Vector3 checkpointForward = checkPointHelper.NextCheckPoint.transform.forward;
        float directionDot = Vector3.Dot(transform.forward, checkpointForward);
        sensor.AddObservation(directionDot);
        //sensor.AddObservation(checkpointForward);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        if (_isEnable)
        {
            float forwardAmount = actions.ContinuousActions[0];
            float turnAmount = actions.ContinuousActions[1];
            bool isBreak = false;

            //AddReward(forwardAmount / 1);

            // if (forwardAmount > 0)
            // {
            //     AddReward(0.1f);
            // }

            carController.UpdateCarInputs(forwardAmount, turnAmount, isBreak);
        }
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

        if (collision.gameObject.CompareTag("Car"))
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

        if (collision.gameObject.CompareTag("Car"))
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


    public void EnableInput(bool enable)
    {
        _isEnable = enable;
    }
}