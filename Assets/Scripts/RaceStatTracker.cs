using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Vehicles.Car;

public class RaceStatTracker : MonoBehaviour
{
    public GameObject FinishLineCollider;
    public int NumberOfLaps;
    public CarController UserCarController;
    public List<MonoBehaviour> AICarAgents;

    public List<GameObject> lights;
    public Material RedLightMaterial;
    public Material GreenLightMaterial;
    public AudioSource RaceCountdown;

    public Canvas GameEndedCanvas;
    public TextMeshProUGUI RaceTimeText;
    public TextMeshProUGUI FastestSpeedText;

    private bool raceStarted = false;
    private int currentLap;
    private float countdown = 0;
    private float raceTime = 0;
    private bool soundPlayed = false;
    private bool gameEnded = false;

    public int CurrentLap
    {
        get
        {
            if (currentLap == 0) return 1;
            if (currentLap >= NumberOfLaps) return NumberOfLaps;
            return currentLap; 
        }
    }

    void Start()
    {
        GameEndedCanvas.enabled = false;
        UserCarController.enabled = false;
        AICarAgents.ForEach(
            (agent) => 
            {
                agent.enabled = false;
            }
        );
    }

    void Update()
    {
        if(!raceStarted)
        {
            countdown += Time.deltaTime;

            if (countdown > 3.75)
            {
                if (!soundPlayed)
                {
                    RaceCountdown.Play();
                    soundPlayed = true;
                }
            }
            if (countdown >= 4)
            {
                lights[0].GetComponent<MeshRenderer>().material = RedLightMaterial;
            }
            if (countdown >= 5)
                lights[1].GetComponent<MeshRenderer>().material = RedLightMaterial;
            if (countdown >= 6)
                lights[2].GetComponent<MeshRenderer>().material = RedLightMaterial;
            if (countdown >= 7)
            {
                lights.ForEach(
                    (light) =>
                    {
                        light.GetComponent<MeshRenderer>().material = GreenLightMaterial;
                    }
                );
                StartRace();
                raceStarted = true;
            }
        } 
        else
        {
            raceTime += Time.deltaTime;
        }
    }

    void StartRace()
    {
        UserCarController.enabled = true;
        AICarAgents.ForEach(
            (agent) =>
            {
                agent.enabled = true;
            }
        );
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.Equals(FinishLineCollider))
        {
            currentLap++;
            if (currentLap >= NumberOfLaps + 1 && !gameEnded)
            {
                GameEndedCanvas.enabled = true;
                RaceTimeText.text = $"{Math.Floor(raceTime / 60)}:{((int)raceTime % 60)}";
                FastestSpeedText.text = ((int)UserCarController.TopSpeed).ToString() + "km/h";
                gameEnded = true;
            }
        }
    }
}
