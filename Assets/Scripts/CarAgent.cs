using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class CarAgent : Agent
{
	private Rigidbody rb;
	int targetCount = 0;
	int targetHitCount = 0;
	public override void Initialize()
	{
		rb = GetComponent<Rigidbody>();
		targetCount = GameObject.FindGameObjectsWithTag("Target").Length;
	}

	public override void OnEpisodeBegin()
	{
		if (this.transform.localPosition.y < 0)
		{
			this.transform.localPosition = new Vector3(377.9497f, 1f, 635.1949f);
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
	public float rotationSpeed = 100f;

	public override void OnActionReceived(ActionBuffers actionBuffers)
	{
		Vector3 controlSignal = Vector3.zero;
		controlSignal.x = actionBuffers.ContinuousActions[1];
		controlSignal.z = -actionBuffers.ContinuousActions[0];

		Vector3 movement = controlSignal.z * speedMultiplier * transform.forward * Time.deltaTime;
		transform.localPosition += movement;

		if (movement.magnitude > 0.01f)
		{
			Vector3 rotate = new Vector3(0f, controlSignal.x * rotationSpeed * Time.deltaTime, 0f);
			transform.Rotate(rotate);
		}

		if (controlSignal.x != 0)
		{
			AddReward(-0.1f);
		}
	}

	public override void Heuristic(in ActionBuffers actionsOut)
	{
		var continuousActionsOut = actionsOut.ContinuousActions;
		continuousActionsOut[0] = Input.GetAxis("Vertical");
		continuousActionsOut[1] = Input.GetAxis("Horizontal");
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.transform.parent.tag != "Track")
		{
			AddReward(-0.1f);
			EndEpisode();
		}
		if (collision.gameObject.tag == "Target")
		{
			Destroy(collision.gameObject);
			AddReward(0.2f);
			targetHitCount++;
			Debug.Log("hit target");
		}

		if(targetHitCount >= targetCount)
		{
			AddReward(1f);
			EndEpisode();
		}
	}

}