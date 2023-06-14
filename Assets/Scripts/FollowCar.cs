using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCar : MonoBehaviour
{
    [SerializeField] GameObject car;
    [SerializeField] Vector3 startPos;

    private void Start()
    {
        if(startPos==null)
            startPos = new Vector3(0.01f, 0.56f, -0.4f);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = car.transform.position; //+ startPos;
        this.transform.rotation = Quaternion.Euler(new Vector3(-car.transform.rotation.x, -car.transform.rotation.y -90, car.transform.rotation.z - 69.838f));
    }
}
