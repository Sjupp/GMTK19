using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    private Rigidbody rb;
    private bool curveBall = false;

    //Arbitrary af
    private Vector3 P1Goal = new Vector3(40,0,0);
    private Vector3 P2Goal = new Vector3(-40,0,0);

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        //ClampSpeed();
        //ShittyCurveMechanic();
    }

    private void ClampSpeed()
    {
        if (rb.velocity.sqrMagnitude > 1000)
        {
            rb.velocity = rb.velocity.normalized * 31;
        }
    }

    private void ShittyCurveMechanic()
    {
        var currentDir = rb.velocity.normalized;
        var targetDir = (P2Goal - rb.position).normalized;

        if (curveBall)
        {
            rb.velocity += targetDir * 0.25f;
        }
    }
}
