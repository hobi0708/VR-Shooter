using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;


public class RollerAgent_AB : Agent
{
    Rigidbody rb;
    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    public override void OnEpisodeBegin()
    {
        if(this.transform.localPosition.y < 0f)
        {
            //agent fell
            this.rb.angularVelocity = Vector3.zero;
            this.rb.linearVelocity = Vector3.zero;
            this.transform.localPosition = new Vector3(0, 0.5f, 0);
        }
        this.transform.localPosition = new Vector3(Random.value * 8 - 4, 0.5f, Random.value * 8 - 4);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // Target and Agent positions
        sensor.AddObservation(target.localPosition);           //3 floats
        sensor.AddObservation(this.transform.localPosition);   //3 floats

        // Agent velocity
        sensor.AddObservation(rb.linearVelocity.x); //1 float
        sensor.AddObservation(rb.linearVelocity.z); //1 float

        //Total 8 floats
    }

    public float forceMultiplier=10;
    public override void OnActionReceived(ActionBuffers action)
    {
        // Actions, size = 2
        Vector3 controlSignal = Vector3.zero;
        controlSignal.x= action.ContinuousActions[0];  //force x
        controlSignal.z= action.ContinuousActions[1];  //force z
        rb.AddForce(controlSignal * forceMultiplier);
        // Rewards
        float distanceToTarget = Vector3.Distance(this.transform.localPosition, target.localPosition);
        // Reached target
        if (distanceToTarget < 1.42f)
        {
            SetReward(1.0f);
            EndEpisode();
        }
        else if (this.transform.localPosition.y < 0f)
        {
            //fell off
            EndEpisode();
        }



    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;
        continuousActionsOut[0] = Input.GetAxis("Horizontal");
        continuousActionsOut[1] = Input.GetAxis("Vertical");
    }





}
