using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{

    [SerializeField]
    private ProjectileData data = null;

    [SerializeField]
    private new Rigidbody rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
    public void Project(Player source, Vector3 direction, float speed)
    {
        data.source = source;
        data.direction = direction;
        data.speed = speed;
    }

    private void Update()
    {
        Move();
    }
    private void Move()
    {
        rigidbody.velocity = data.direction * data.speed;
    }

}