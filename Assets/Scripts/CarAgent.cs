using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using System.Collections.Generic;

public class CarAgent : Agent
{
	private Rigidbody rb;
	List<GameObject> targets = new List<GameObject>();
	int targetCount = 0;
	int targetHitCount = 0;
	public override void Initialize()
	{
		rb = GetComponent<Rigidbody>();
		targetCount = GameObject.FindGameObjectsWithTag("Target").Length;
	}

	public override void OnEpisodeBegin()
	{
		foreach (var target in targets)
		{
			target.SetActive(true);
		}
		//if (this.transform.localPosition.y < 0)
		//{
			this.transform.position = new Vector3(315.2f, 5.64f, 719.7f);
			this.transform.rotation= new Quaternion(0f,180f,0f,0f);
		//}
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

		Vector3 movement = new Vector3(0f,0f, controlSignal.z * speedMultiplier * Time.deltaTime*1000);
		Debug.Log(movement);
		this.gameObject.GetComponent<Rigidbody>().AddRelativeForce(transform.forward * movement.z, ForceMode.Acceleration);

		//transform.localPosition += movement;
		Debug.Log(transform.position.y);
		if(transform.position.y < 5.0f)
		{
			AddReward(-0.2f);
			EndEpisode();
		}
		if (movement.magnitude > 0.01f)
		{
			Vector3 rotate = new Vector3(0f, controlSignal.x * rotationSpeed * Time.deltaTime, 0f);
			transform.Rotate(rotate);
		}

		if (controlSignal.x != 0)
		{
			AddReward(-0.1f);
		}

		if (Mathf.Abs(controlSignal.x) > 20f)
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
		if (collision.gameObject.transform.parent.tag != "Track" && collision.gameObject.transform.parent.tag != "Target")
		{
			AddReward(-0.1f);
			EndEpisode();
		}
		if (collision.gameObject.tag == "Target")
		{
			collision.gameObject.SetActive(false);
			targets.Add(collision.gameObject);
			AddReward(0.2f);
			targetHitCount++;
			Debug.Log("hit target");
		}
		if (collision.gameObject.tag == "Terrain")
		{
			AddReward(-0.2f);
			EndEpisode();
			Debug.Log("hit terrain");
		}

		if (targetHitCount >= targetCount)
		{
			AddReward(1f);
			EndEpisode();
		}
	}

}