using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleAI : MonoBehaviour
{
    public GameObject AI_cars;

    public void ToggleCars()
    {
        if(AI_cars.activeSelf)
            AI_cars.SetActive(false);
        else 
            AI_cars.SetActive(true);
    }
}
