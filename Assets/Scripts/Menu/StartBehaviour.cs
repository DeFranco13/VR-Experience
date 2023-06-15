using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartBehaviour : MonoBehaviour
{
    public TrackSelector trackSelector;
    public CarSelector carSelector;
    public Toggle spectateToggle;

    public void Play()
    {
        //Car
        PlayerPrefs.SetString("SelectedCar", carSelector.GetSelectedCarName());

        //Track
        string sceneName = trackSelector.GetSelectedTrackName();
        if (spectateToggle.isOn)
            sceneName += " - Spectator";
        SceneManager.LoadScene(sceneName);
    }
}
