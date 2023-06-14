using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PhysicsButton : MonoBehaviour
{
    public UnityEvent onPressed, onReleased;
    [SerializeField] private float threshold = 0.1f;
    [SerializeField] private float deadZone = 0.015f;

    private bool isPressed;
    private Vector3 startPosition;
    private ConfigurableJoint joint;

    void Start()
    {
        startPosition= transform.localPosition;
        joint = GetComponent<ConfigurableJoint>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isPressed && GetValue() + threshold>=1) 
            Pressed();
        if (isPressed && GetValue() - threshold <= 0)
            Released();
    }

    private void Pressed()
    {
        isPressed= true;
        onPressed.Invoke();
    }

    private void Released() 
    { 
        isPressed= false;
        onReleased.Invoke();
    }

    private float GetValue()
    {
        var value = Vector3.Distance(startPosition, transform.localPosition)/joint.linearLimit.limit;

        if (Math.Abs(value) < deadZone)
            value = 0;
        return Math.Clamp(value, -1f, 1f);
    }
}
