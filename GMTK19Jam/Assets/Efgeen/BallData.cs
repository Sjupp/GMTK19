using System;
using UnityEngine;

public enum BallState
{
    Grounded,
    Aired
}

[Serializable]
public class BallData
{

    public BallState state = BallState.Grounded;

    public Rigidbody rigidbody;

    public Player source;

    public Vector3 direction;
    public float speed;

    public float verticalVelocity;
    public float gravity;

    public float decrementAmount = 0.15f;

    public Vector3 rotationDirection;

}