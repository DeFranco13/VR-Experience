using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabSpin : MonoBehaviour
{
    public float Speed = 15f;
    public float bobSpeed = 1f;
    public float bobHeight = 0.1f;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        transform.Rotate(Vector3.up, Speed * Time.deltaTime);

        Vector3 bobPosition = startPosition + Vector3.up * Mathf.Sin(Time.time * bobSpeed) * bobHeight;
        transform.position = bobPosition;
    }
}
