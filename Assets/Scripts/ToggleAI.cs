using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleAI : MonoBehaviour
{
    public GameObject AIcars;

    public void ToggleCars()
    {
        if(AIcars.activeSelf)
            AIcars.SetActive(false);
        else 
            AIcars.SetActive(true);
    }
}
