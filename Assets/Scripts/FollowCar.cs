using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCar : MonoBehaviour
{
    [SerializeField] GameObject buttonPlaceholder;

    // Update is called once per frame
    void Update()
    {
        transform.position = buttonPlaceholder.transform.position;
        transform.localRotation = transform.rotation = Quaternion.Euler(buttonPlaceholder.transform.eulerAngles.x, buttonPlaceholder.transform.eulerAngles.y + 90f, buttonPlaceholder.transform.eulerAngles.z - 69.838f);
    }
}
