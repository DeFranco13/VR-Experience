using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMovement : MonoBehaviour
{
    private bool isPlayerTouching = false;
    [SerializeField] Rigidbody rigidbody;

    private void OnCollisionEnter(Collision collision)
    {
        isPlayerTouching= true;
    }

    private void OnCollisionExit(Collision collision)
    {
        isPlayerTouching= false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerTouching)
        {
            rigidbody.constraints = RigidbodyConstraints.None;
            rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        }
        else
        {
            rigidbody.constraints = RigidbodyConstraints.FreezePositionY;
        }
    }
}
