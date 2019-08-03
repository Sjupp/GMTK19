using System;
using UnityEngine;

public enum PlayerState
{

    None = 0,
    Knockback = 1 << 0,

}

[Serializable]
public class PlayerData
{

    //State
    public PlayerState state;

    //Movement
    public Vector3 normalizedMovementDirection = Vector3.zero;
    public float movementSpeed = 10f;

    //Rotation
    public float rotationSpeed = 1000f;

    //Knockback
    public float knockbackPower = 100f;
    public float knockbackDuration = 0.05f;
    public float knockbackTimer = 0.05f;

    public int ammo = 2;
    private int maxAmmo = 2;

}