using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class CarAgentRays : Agent
{
	public Transform Target;
	public float speedMultiplier = 0.5f;
	public float rotationMultiplier = 5; 
	//public float speedMultiplier = 0.1f;

	public override void OnEpisodeBegin()
	{
		if (this.transform.localPosition.y < 0)
		{
			this.transform.localPosition = new Vector3(0, 0.5f, 0);
			this.transform.localRotation = Quaternion.identity;
		}
	}
	public override void Heuristic(in ActionBuffers actionsOut)
	{
		var continuousActionsOut = actionsOut.ContinuousActions;
		continuousActionsOut[0] = Input.GetAxis("Vertical");
		continuousActionsOut[1] = Input.GetAxis("Horizontal");
	}
	public override void CollectObservations(VectorSensor sensor)
	{
		// Agent positie
		sensor.AddObservation(this.transform.localPosition);
	}
	public override void OnActionReceived(ActionBuffers actionBuffers)
	{     // Acties, size = 2 
		  Vector3 controlSignal = Vector3.zero;
		controlSignal.z = actionBuffers.ContinuousActions[0];
		transform.Translate(controlSignal * this.speedMultiplier);
		transform.Rotate(0.0f, this.rotationMultiplier* actionBuffers.ContinuousActions[1], 0.0f);
		// Beloningen
		    float distanceToTarget = Vector3.Distance(this.transform.localPosition, Target.localPosition);
		// target bereikt
		   if (distanceToTarget < 1.42f)
		{
			SetReward(1.0f);
			EndEpisode();
		}    
		  // Van het platform gevallen?
		      else if (this.transform.localPosition.y < 0)    {
			EndEpisode();
		}
	}



	}