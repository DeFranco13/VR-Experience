using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetHandPosition : MonoBehaviour
{
    public Transform attach;
    private bool grabbed = false;
    
    void Update()
    {
        if (grabbed)
            this.transform.position = attach.position;
    }
    public void SetHand(Transform attach)
    {
        grabbed = true;
        this.attach = attach;

    }

    public void RemoveHand(Transform controller)
    {
        grabbed= false;
        this.transform.position = controller.position;
    }
}
