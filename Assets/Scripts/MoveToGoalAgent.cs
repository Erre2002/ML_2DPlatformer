using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;


public class MoveToGoalAgent : Agent
{
    [SerializeField] private Transform TargetTransform;
    [SerializeField] private Material winMaterial;
    [SerializeField] private Material loseMaterial;
    [SerializeField] private MeshRenderer floorMeshRenderer;
    private PlayerState playerstate;
    public PlayerMovement playermovement;
    public float moveX;
    public float moveY;

    public override void OnEpisodeBegin()
    {
        transform.localPosition = new Vector3(6.45f, 1.38f, 0f);

        // base.OnEpisodeBegin();
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(TargetTransform.localPosition);
        //base.CollectObservations(sensor);

    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        //Debug.Log(actions.DiscreteActions[0]);
        moveX = actions.ContinuousActions[0];
        moveY = actions.ContinuousActions[1];
        Debug.Log(moveY);
        Debug.Log(moveX);

        float moveSpeed = 3f;
        if(moveY == -1f){
            playermovement.isJumpPressed = true;
            Debug.Log("jumped");
        }
        moveY = 0f;
        transform.localPosition += new Vector3(moveX, moveY, 0) * Time.deltaTime * moveSpeed;
        
        

        // base.OnActionReceived(actions);  
    }
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continousActions = actionsOut.ContinuousActions;
        continousActions[0] = Input.GetAxisRaw("Horizontal");
        continousActions[1] = Input.GetAxisRaw("Vertical");
        //base.Heuristic(actionsOut);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Goal")
        {
            SetReward(1000f);
            floorMeshRenderer.material = winMaterial;
            EndEpisode();
        }
        if (other.gameObject.tag == "Wall")
        {
            SetReward(-100f);
            floorMeshRenderer.material = loseMaterial;
            EndEpisode();
        }

    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, TargetTransform.position);
        
        if (distance <= 80)
        {
            SetReward(5f);
        }
        if (distance <= 50)
        {
            SetReward(10f);
        }
        if (distance <= 30)
        {
            SetReward(30f);
        }

        if (distance <= 10)
        {
            SetReward(50f);
        }
    }
}
