using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

public class ShowSpeed : MonoBehaviour
{
    public TextMeshProUGUI text;
    public CarController controller;
    // Update is called once per frame
    void Update()
    {
        updateSpeed();
    }

    private void updateSpeed()
    {
        text.text = ((int)controller.CurrentSpeed).ToString() + " km/u";
    }
}
