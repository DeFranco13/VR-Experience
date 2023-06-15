using System;
using UnityEngine;
using UnityEngine.XR;

public class XRCameraSwitcher : MonoBehaviour
{
    public GameObject[] CameraRigs;
    private int currentCameraIndex = 0;

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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            nextCameraRig();
        }
    }

    private void previousCameraRig()
    {
        disabledCameraRig(currentCameraIndex);
        currentCameraIndex = (currentCameraIndex - 1) % CameraRigs.Length;
        activateCameraRig(currentCameraIndex);
    }

    private void nextCameraRig()
    {
        disabledCameraRig(currentCameraIndex);
        currentCameraIndex = (currentCameraIndex + 1) % CameraRigs.Length;
        activateCameraRig(currentCameraIndex);
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