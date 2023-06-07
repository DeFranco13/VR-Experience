using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using System.Collections.Generic;
using System;

namespace UnityStandardAssets.Vehicles.Car
{
	[RequireComponent(typeof(CarController))]
	public class CarAgent : Agent
	{
		private CarController carController;
		int toHit = 1;
		private Rigidbody rb;
		List<GameObject> targets = new List<GameObject>();
		int targetCount = 0;
		int targetHitCount = 0;
		public override void Initialize()
		{
			rb = GetComponent<Rigidbody>();
			targetCount = GameObject.FindGameObjectsWithTag("Target").Length;
			carController = GetComponent<CarController>();
		}

		public override void OnEpisodeBegin()
		{
			toHit = 1;
			foreach (var target in targets)
			{
				target.SetActive(true);
			}
			this.transform.position = new Vector3(382.6f, 1.1f, 625.82f);
			this.transform.rotation = new Quaternion(0f, 180f, 0f, 0f);
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
			controlSignal.z = -actionBuffers.ContinuousActions[0];
			controlSignal.x = actionBuffers.ContinuousActions[1];
			controlSignal.y = actionBuffers.ContinuousActions[2];
			carController.Move(controlSignal.z, controlSignal.x, controlSignal.x, controlSignal.y);
			//Vector3 movement = new Vector3(0f,0f, controlSignal.z * speedMultiplier * Time.deltaTime*1000);
			//Debug.Log(movement);
			//this.gameObject.GetComponent<Rigidbody>().AddRelativeForce(transform.forward * movement.z, ForceMode.Acceleration);

			//transform.localPosition += movement;
			if (transform.position.y < 5.0f)
			{
				AddReward(-0.2f);
				EndEpisode();
			}
			//if (movement.magnitude > 0.01f)
			//{
			//	Vector3 rotate = new Vector3(0f, controlSignal.x * rotationSpeed * Time.deltaTime, 0f);
			//	transform.Rotate(rotate);
			//}

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
			continuousActionsOut[0] = Input.GetAxis("Horizontal");
			continuousActionsOut[1] = Input.GetAxis("Vertical");
			continuousActionsOut[2] = Input.GetAxis("Jump");
			//Debug.Log(continuousActionsOut);
			//continuousActionsOut[0] = Input.GetAxis("Vertical");
			//continuousActionsOut[1] = Input.GetAxis("Horizontal");
		}

		private void OnTriggerEnter(Collider other)
		{
			string name = "";
			try
			{
				name = other.transform.parent.name.Split('(')[1];
				name = name.Substring(0, name.Length - 1);
			}
			catch (Exception)
			{
			}

			if (other.tag.Trim() == "Target" && name == toHit.ToString())
			{
				other.gameObject.SetActive(false);
				targets.Add(other.gameObject);
				AddReward(0.2f);
				targetHitCount++;
				Debug.Log("add hit");
				toHit++;
				Debug.Log("hit target");
			}
			else
			{
				if (name == "target" || name == "")
				{
					AddReward(0.2f);
				}
				else
				{
					Debug.Log("wrong target");
					AddReward(-0.1f);
					EndEpisode();
				}
			}
		}


		private void OnCollisionEnter(Collision collision)
		{
			if (collision.gameObject.transform.parent.tag != "Track")
			{
				AddReward(-0.1f);
				EndEpisode();
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
}