using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class CarAgent : Agent
{
	private Rigidbody rb;
	public override void Initialize()
	{
		rb = GetComponent<Rigidbody>();
		Debug.Log("initialized");
	}
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
		Debug.Log("received");
		// Acties
		Vector3 controlSignal = Vector3.zero;
		controlSignal.x = actionBuffers.ContinuousActions[0];
		controlSignal.z = actionBuffers.ContinuousActions[1];
		rb.AddForce(controlSignal * speedMultiplier);

	}
	public override void Heuristic(in ActionBuffers actionsOut)
	{
		var continuousActionsOut = actionsOut.ContinuousActions;
		continuousActionsOut[0] = Input.GetAxis("Vertical");
		continuousActionsOut[1] = Input.GetAxis("Horizontal");
	}

}
