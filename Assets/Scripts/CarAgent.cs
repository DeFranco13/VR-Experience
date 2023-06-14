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
		int toHit = 0;
		private Rigidbody rb;
		List<GameObject> targets = new List<GameObject>();
		int targetCount = 0;
		int targetHitCount = 0;

		// collide bools
		bool collidedWithTarget = false;
        bool collidedWithTerainOrElse = false;


        public override void Initialize()
		{
			rb = GetComponent<Rigidbody>();
			targetCount = GameObject.FindGameObjectsWithTag("Target").Length;
			Debug.Log($"targets to hit {targetCount}");
			carController = GetComponent<CarController>();
		}

		public override void OnEpisodeBegin()
		{
			toHit = 0;
			targetHitCount = 0;
			toHit = 0;
			carController.Move(0, 0, 0, 0);
			//set the targets active again every new episode
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

		private float speedMultiplier = 10f;
		private float rotationSpeed = 10f;

		public override void Heuristic(in ActionBuffers actionsOut)
		{
			var continuousActionsOut = actionsOut.ContinuousActions;
			continuousActionsOut[0] = Input.GetAxis("Horizontal");
			continuousActionsOut[1] = Input.GetAxis("Vertical");
			continuousActionsOut[2] = Input.GetAxis("Jump");

		}
		public override void OnActionReceived(ActionBuffers actionBuffers)
		{
			Vector3 controlSignal = Vector3.zero;

			controlSignal.x = actionBuffers.ContinuousActions[0];
            controlSignal.z = actionBuffers.ContinuousActions[1];
            controlSignal.y = actionBuffers.ContinuousActions[2];


			controlSignal.x = controlSignal.x * 10;
			controlSignal.z = controlSignal.z * 10;


            //beweeg de auto
            carController.Move(controlSignal.x, controlSignal.z, 0f,controlSignal.y);
			
            /*
			 * 1) moest de auto voor een of andere reden naar boven bewegen stoppen we de episode
			 * 2) we geven een positieve reward wanneer de auto een target hit en als hij een ronde heeft uitgereden
			 * 3) we geven een negatieve reward wanneer hij de zijmuren raakt 
			 * of er door zou rijden en van de track zo gaan
			*/


            if (transform.position.y > 10.0f)
			{
				AddReward(-0.2f);
				EndEpisode();
				Debug.Log("too high");
			}

            if (collidedWithTarget)
            {
				targetHitCount++;
				toHit++;

				AddReward(0.5f);

                if (targetHitCount >= targetCount)
                {
                    AddReward(5f);
                    EndEpisode();
                }

				collidedWithTarget = false;
            }
        }

		private void OnTriggerEnter(Collider other)
		{

			string name = "";
			int targetNumber = 0;
			try
			{
				name = other.transform.parent.name.Split('(')[1];
				name = name.Substring(0, name.Length - 1);
				targetNumber = int.Parse(name);
			}
			catch (Exception)
			{
			}

			//Debug.Log("target " + toHit.ToString());
            //Debug.Log(targetNumber);

            if (targetNumber == toHit)
			{
                Debug.Log("hit target");

                targets.Add(other.gameObject);
                collidedWithTarget = true;
                other.gameObject.SetActive(false);
			}
        }


		private void OnCollisionEnter(Collision collision)
		{
            if (collision.gameObject.transform.tag == "TrackObject")
			{
				Debug.Log("hit wall");
                AddReward(-0.5f);
			}
		}

		private float wallCollisionTimer = 0.0f;
		private float wallCollisionThreshold = 10.0f;
		private bool isWallCollisionTimerRunning = false;

		private void OnCollisionStay(Collision collision)
		{
			if (collision.gameObject.transform.tag == "TrackObject")
			{
				Debug.Log("keeps hitting wall");
				AddReward(-0.05f);

				// Start or update the wall collision timer
				if (!isWallCollisionTimerRunning)
				{
					wallCollisionTimer = 0.0f;
					isWallCollisionTimerRunning = true;
				}
				else
				{
					wallCollisionTimer += Time.fixedDeltaTime;
				}

				// Check if the wall collision timer has reached the threshold
				if (wallCollisionTimer >= wallCollisionThreshold)
				{
					AddReward(-0.05f);
					wallCollisionTimer = 0.0f;
					Debug.Log("episode ended");
					EndEpisode();
				}
			}
		}


	}
}