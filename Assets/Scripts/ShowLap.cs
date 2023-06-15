using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

public class ShowLap : MonoBehaviour
{
    public TextMeshProUGUI text;
    public RaceStatTracker tracker;
    // Update is called once per frame
    void Update()
    {
        updateLap();
    }

    private void updateLap()
    {
        text.text = "Lap " + tracker.CurrentLap + "/" + tracker.NumberOfLaps ;
    }
}
