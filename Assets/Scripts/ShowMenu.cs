using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowMenu : MonoBehaviour
{
    public XRCameraSwitcher XRCameraSwitcher;
    public Canvas canvas;
    public GameObject[] canvasPlaceHolders;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Joystick2Button1))
            ShowMiniMenu();
    }
    public void ShowMiniMenu()
    {
        if(canvas.isActiveAndEnabled)
        {
            canvas.enabled = false;
            
        } else if(!canvas.isActiveAndEnabled) 
        { 
            canvas.enabled = true;
            canvas.transform.position = canvasPlaceHolders[XRCameraSwitcher.currentCameraIndex].transform.position - new Vector3(0,0.20f,0);
            canvas.transform.rotation = XRCameraSwitcher.CameraRigs[XRCameraSwitcher.currentCameraIndex].transform.rotation;
        }
    }
}
