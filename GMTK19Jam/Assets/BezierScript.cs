using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierScript : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public GameObject ball;

    public Transform p0, p1, p2, p3;
    public int pointAmt = 10; //this will always be +1

    private float t;
    private Vector3[] positions;

    public float speedModifier = 1.0f;
    private float timer = 0;

    private void Start()
    {
        positions = new Vector3[pointAmt + 1];
        lineRenderer.positionCount = pointAmt + 1;
        //DrawLinearCurve();
        //DrawCubicCurve();
    }

    private void OnKick()
    {
        //kickedPos (p0)
        //kickedDir = direction * strength(?) (p1)
        //kickedGoal (p2)

        //CreateCurve(p0, p1, p2);

        //var mod = DistanceAndAngleModifier(); (0 - 100)

        //StartAlongPath(mod);

        //onCollision or if it runs out on its own, Project


    }

    private void Update()
    {
        FollowCurve();
        DrawQuadraticCurve();
    }

    private void FollowCurve()
    {
        timer += Time.deltaTime * speedModifier;

        ball.transform.position = CalculateQuadraticBezierPoint(timer, p0.position, p1.position, p2.position);

        if (timer >= 1f)
        {
            timer = 0;
            ball.transform.position = positions[0];
        }
    }

    private void DrawLinearCurve()
    {
        for (int i = 1; i < pointAmt + 1; i++)
        {
            t = i / (float)pointAmt;
            positions[i] = CalculateLinearBezierPoint(t, p0.position, p1.position);
            Debug.Log(positions[i]);
        }
        lineRenderer.SetPositions(positions);
    }

    private void DrawQuadraticCurve()
    {
        for (int i = 0; i < pointAmt + 1; i++)
        {
            t = (float)i / pointAmt;
            positions[i] = CalculateQuadraticBezierPoint(t, p0.position, p1.position, p2.position);
        }
        lineRenderer.SetPositions(positions);
    }

    private void DrawCubicCurve()
    {
        for (int i = 1; i < pointAmt + 1; i++)
        {
            t = (float)i / pointAmt;
            positions[i] = CalculateCubicBezierPoint(t, p0.position, p1.position, p2.position, p3.position);
        }
        lineRenderer.SetPositions(positions);
    }


    private Vector3 CalculateLinearBezierPoint(float t, Vector3 p0, Vector3 p1)
    {
        return p0 + t * (p1 - p0);
    }

    private Vector3 CalculateQuadraticBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1 - t;

        Vector3 v = Mathf.Pow(u, 2) * p0;
        v += 2 * u * t * p1;
        v += Mathf.Pow(t, 2) * p2;

        return v;
    }

    private Vector3 CalculateCubicBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        float u = 1 - t;

        Vector3 v = Mathf.Pow(u, 3) * p0;
        v += 3 * Mathf.Pow(u, 2) * t * p1;
        v += 3 * u * Mathf.Pow(t, 2) * p2;
        v += Mathf.Pow(t, 3) * p3;

        //var v1 = Mathf.Pow((1 - t), 3) * p0;
        //v1 += 3 * Mathf.Pow(1 - t, 2) * t * p1;
        //v1 += 3 * (1 - t) * Mathf.Pow(t, 2) * p2;
        //v1 += Mathf.Pow(t, 3) * p3;

        return v;
    }

}
