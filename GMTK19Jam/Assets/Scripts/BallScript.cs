using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{

    private Vector3 P1Goal = new Vector3(40,0,10);
    private Vector3 P2Goal = new Vector3(-40,0,10);
    private Vector3 targetGoal;
    private Rigidbody rb;

    public float curveBallThreshold = 15.0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!(collision.collider.CompareTag("Player1") || collision.collider.CompareTag("Player2")))
        {
            Debug.Log("hit something else");
            return;
        }

        //if hit by player projectile
        //Debug
        Debug.Log("impulse: " + collision.impulse);
        Debug.Log("contacts: " + collision.GetContacts(collision.contacts));
        Debug.Log("contact point: " + collision.GetContact(0).point);
        Debug.Log("contactCount: " + collision.contactCount);

        //New direction from collision
        var newDir = collision.impulse.normalized;
        Debug.DrawRay(collision.GetContact(0).point, newDir, Color.red, 5.0f);

        GetTargetGoal(collision);

        var goalDir = (targetGoal - transform.position).normalized;
        Debug.DrawRay(collision.GetContact(0).point, goalDir, Color.blue, 5.0f);

        if (Vector3.Angle(newDir, goalDir) < curveBallThreshold)
        {
            Debug.Log("Angle diff: " + Vector3.Angle(newDir, goalDir));
            //StartCoroutine(CurveBall(targetGoal));
            rb.velocity = goalDir * 10;
        }

    }

    private Vector3 GetTargetGoal(Collision collision)
    {
        //If a projectile belonging to player1 hit the ball
        if (collision.collider.CompareTag("Player1"))
        {
            targetGoal = P2Goal;
        }
        else
        {
            targetGoal = P1Goal; ;
        }
        return targetGoal;
    }

    public IEnumerator CurveBall(Vector3 v3)
    {
        Debug.Log("Hej");
        for (int i = 0; i < 200; i++)
        {
            var hej = (targetGoal - transform.position).normalized;
            rb.AddForce(hej * 25);
            yield return null;
        };
    }

    private void Test()
    {
        var dir1 = new Vector3(10, 0, 0);
        var dir2 = new Vector3(0, 0, 10);
    }
}
