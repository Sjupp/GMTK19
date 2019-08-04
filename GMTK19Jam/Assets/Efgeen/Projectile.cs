﻿using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{

    [SerializeField]
    private ProjectileData data = null;

    private void Awake()
    {
        data.rigidbody = GetComponent<Rigidbody>();
    }

    public void Project(Player source, Vector3 direction, float speed)
    {
        transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
        ServiceLocator.GetAudio().PlaySound("Player_Projectile");
        CameraShake.INSTANCE?.Shake(15f, 0.1f);
        data.source = source;
        data.direction = direction.normalized;
        data.speed = speed;
    }

    private void Update()
    {
        Move();
    }
    private void Move()
    {
        data.rigidbody.velocity = data.direction * data.speed;
    }

    public void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Untagged"))
        {
            Debug.Log(collider);
            return;
        }

        if (collider.CompareTag("Ball"))
        {
            collider.GetComponent<Ball>().Project(data.source, data.direction, 50f);
            collider.GetComponent<Ball>().Data.decrementAmount = 0.15f;
        }

        if (collider.CompareTag("Player"))
        {
            Player player = collider.GetComponent<Player>();
            if (player.team == data.source.team)
            {
                return;
            }
            else
            {
                player.ApplyKnockback(data.direction, 50f);
            }
        }
        ServiceLocator.GetAudio().PlaySound("Explosion");
        CameraShake.INSTANCE?.Shake(50f, 0.1f);
        Destroy(gameObject);
    }

}