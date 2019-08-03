using System;
using UnityEngine;

public enum ProjectileState
{
    None = 0,
    Moving = 1 << 0
}

[Serializable]
public class ProjectileData
{

    public ProjectileState state = ProjectileState.None;

    public Player source;

    public Vector3 direction;
    public float speed;

    public float projectDuration;
    public float projectTimer;

    public Rigidbody rigidbody;

}