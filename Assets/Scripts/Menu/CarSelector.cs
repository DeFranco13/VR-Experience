using System;
using UnityEngine;

public class CarSelector : MonoBehaviour
{
    public Selector carSelector;

    public String GetSelectedCarName()
    {
        return carSelector.CurrentPrefab.name;
    }
}