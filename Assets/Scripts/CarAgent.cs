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
			this.transform.localRotation = Quaternion.identity;
		}
	}

	public override void CollectObservations(VectorSensor sensor)
	{
		// Target en Agent posities
		sensor.AddObservation(this.transform.localPosition);
		sensor.AddObservation(this.transform.localRotation);
	}

	public float speedMultiplier = 0.5f;
	public float rotationSpeed = 100f; // New variable for rotation speed

	public override void OnActionReceived(ActionBuffers actionBuffers)
	{
		// Acties
		Vector3 controlSignal = Vector3.zero;
		controlSignal.x = actionBuffers.ContinuousActions[1];
		controlSignal.z = -actionBuffers.ContinuousActions[0];

		// Apply forward or backward movement
		Vector3 movement = controlSignal.z * speedMultiplier * transform.forward * Time.deltaTime;
		transform.localPosition += movement;

		// Rotate the car only if it's moving
		if (movement.magnitude > 0.01f)
		{
			// Calculate the rotation based on the input
			Vector3 rotate = new Vector3(0f, controlSignal.x * rotationSpeed * Time.deltaTime, 0f);
			transform.Rotate(rotate);
		}
	}

	public override void Heuristic(in ActionBuffers actionsOut)
	{
		var continuousActionsOut = actionsOut.ContinuousActions;
		continuousActionsOut[0] = Input.GetAxis("Vertical");
		continuousActionsOut[1] = Input.GetAxis("Horizontal");
	}
}