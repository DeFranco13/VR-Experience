using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartBehaviour : MonoBehaviour
{
    public TrackSelector trackSelector;
    public CarSelector carSelector;

    void Update()
    {
        //Debug.Log(trackSelector.GetSelectedTrackName() + " | " + carSelector.GetSelectedCarName());
    }

    public void Play()
    {
        //Car
        PlayerPrefs.SetString("SelectedCar", carSelector.GetSelectedCarName());

        //Track
        string sceneName = trackSelector.GetSelectedTrackName();
        SceneManager.LoadScene(sceneName);
    }
}
