using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using Unity.VisualScripting;
using System.Threading;

public class CarAgent : Agent
{
	public override void OnEpisodeBegin()
	{
		if (this.transform.localPosition.y < 0)
		{
			this.transform.localPosition = new Vector3(0, 0.5f, 0);
			this.transform.localRotation = new Quaternion(0, 90, 0, 0);
			this.transform.localRotation = Quaternion.identity;
		}
	}
	public override void CollectObservations(VectorSensor sensor)
	{
		// Target en Agent posities
		sensor.AddObservation(this.transform.localPosition);

	}
	public float speedMultiplier = 0.1f;
	public override void OnActionReceived(ActionBuffers actionBuffers)
	{
		// Acties, size = 2
		Vector3 controlSignal = Vector3.zero;
		controlSignal.x = actionBuffers.ContinuousActions[0];
		controlSignal.z = actionBuffers.ContinuousActions[1];
		transform.Translate(controlSignal * speedMultiplier);


	}
	public override void Heuristic(in ActionBuffers actionsOut)
	{
		var continuousActionsOut = actionsOut.ContinuousActions;
		continuousActionsOut[0] = Input.GetAxis("Vertical");
		continuousActionsOut[1] = Input.GetAxis("Horizontal");
	}

}
