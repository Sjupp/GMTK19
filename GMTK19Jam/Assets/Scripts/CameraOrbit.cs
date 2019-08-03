using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOrbit : MonoBehaviour
{
    public Transform target;

    //public Vector3 distanceFromTarget;
    public float distanceFromTarget = 10;
    public float height = 10;
    public float speed = 5;
    float x = 0;
    float z = 0;
    float time = 0;

    void Update()
    {
        time += Time.deltaTime * speed;
        x = Mathf.Cos(time);
        z = Mathf.Sin(time);

        transform.position = target.transform.position + new Vector3(x, 0, z) * distanceFromTarget + new Vector3(0, height  , 0);

        transform.LookAt(target);
    }
}
