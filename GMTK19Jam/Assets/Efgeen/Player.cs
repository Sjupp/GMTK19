﻿using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{

    [SerializeField]
    private PlayerConfig config = null;

    [SerializeField]
    private PlayerData data = null;

    [SerializeField]
    private new Rigidbody rigidbody;

    [SerializeField]
    private Projectile projectile;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    public void Upd8()
    {

        if ((data.state & PlayerState.Knockback) == PlayerState.Knockback)
        {

            data.knockbackTimer -= Time.deltaTime;

            if (data.knockbackTimer <= 0)
            {
                data.state &= ~PlayerState.Knockback;
            }

        }

        if (data.state == PlayerState.None)
        {
            Move();
        }

    }
    private void Move()
    {
        rigidbody.velocity = data.normalizedMovementDirection * data.movementSpeed;
    }

    public void OnMove(Vector3 direction)
    {
        data.normalizedMovementDirection = direction.normalized;
        Rotate(direction);
    }
    private void Rotate(Vector3 direction)
    {

        if (direction == Vector3.zero)
        {
            return;
        }

        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direction), data.rotationSpeed * Time.deltaTime);

    }

    public void OnKick()
    {

        if (Kick())
        {
            Debug.Log("ONKICK = KICK");
            return;
        }

        Debug.Log("ONKICK = SHOOT");
        Shoot();

    }
    private bool Kick()
    {

        bool hit = false;

        Collider[] overlappingColliders = Physics.OverlapBox(transform.position, new Vector3(2, 2, 2));

        for (int i = 0; i < overlappingColliders.Length; i++)
        {

            if (overlappingColliders[i].tag == "Ball")
            {

                Ball ball = overlappingColliders[i].GetComponent<Ball>();

                ball.Project(this, new Vector3((ball.transform.position.x - transform.position.x), 5f, (ball.transform.position.z - transform.position.z)).normalized, 75f);

                hit = true;
                continue;
            }

            if (overlappingColliders[i].tag == "Player")
            {

                Player kicked = overlappingColliders[i].GetComponent<Player>();

                if (kicked == this)
                {
                    continue;
                }

                kicked.ApplyKnockback((kicked.transform.position - transform.position).normalized, data.knockbackPower);
                hit = true;

            }

        }

        return hit;

    }
    private void Shoot()
    {
        Projectile temp = Instantiate(projectile, transform.position, transform.rotation, null);
        //temp.Project(this, transform.rotation * Vector3.forward, 50);

        //HACK
        //temp.Project(this, (FindObjectOfType<Ball>().transform.position - transform.position), 50);
        //Destroy(temp.gameObject, 0.5f);
    }

    public void OnDash()
    {
        Debug.Log("[Player.Dash] : Dashed");
    }


    public void ApplyKnockback(Vector3 direction, float power)
    {
        data.state |= PlayerState.Knockback;
        data.knockbackTimer = data.knockbackDuration;
        rigidbody.velocity = direction * power;
    }

}
