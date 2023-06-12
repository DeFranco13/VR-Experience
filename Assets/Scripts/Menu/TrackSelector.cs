using System;
using UnityEngine;

public class TrackSelector : MonoBehaviour
{
    public Selector trackSelector;

    public String GetSelectedTrackName()
    {
        return trackSelector.CurrentPrefab.name;
    }
}