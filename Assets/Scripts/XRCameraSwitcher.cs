using System;
using UnityEngine;

public class XRCameraSwitcher : MonoBehaviour
{
    public GameObject[] CameraRigs;
    [NonSerialized] public int currentCameraIndex = 0;
    public Canvas canvas;

    private void Start()
    {
        foreach(GameObject rig in CameraRigs)
        {
            rig.SetActive(false);
        }
        activateCameraRig(currentCameraIndex);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button2))
        {
            nextCameraRig();
        }

        if (Input.GetKeyDown(KeyCode.Joystick2Button0))
        {
            previousCameraRig();
        }
    }

    private void previousCameraRig()
    {
        disabledCameraRig(currentCameraIndex);
        if (currentCameraIndex == 0)
            currentCameraIndex = 7;
        currentCameraIndex = (currentCameraIndex - 1) % CameraRigs.Length;
        activateCameraRig(currentCameraIndex);
        canvas.enabled= false;
    }

    private void nextCameraRig()
    {
        disabledCameraRig(currentCameraIndex);
        currentCameraIndex = (currentCameraIndex + 1) % CameraRigs.Length;
        activateCameraRig(currentCameraIndex);
        canvas.enabled = false;
    }

    private void activateCameraRig(int cameraRigIndex)
    {
        CameraRigs[cameraRigIndex].gameObject.SetActive(true);
    }

    private void disabledCameraRig(int cameraRigIndex)
    {
        CameraRigs[cameraRigIndex].gameObject.SetActive(false);
    }
}