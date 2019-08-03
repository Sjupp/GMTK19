using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Legspawner : MonoBehaviour
{

    private Collider coll;

    public bool onCooldown = false;

    public float maxTimer;

    public float timer;

    private void Awake()
    {
        coll = GetComponent<Collider>();
    }

    public void Update()
    {

        if (onCooldown)
        {

            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                onCooldown = false;
                GetComponent<MeshRenderer>().enabled = true;
            }

        }

    }

    private void OnTriggerEnter(Collider collider)
    {

        if (collider.CompareTag("Player"))
        {

            if (collider.GetComponent<Player>().Data.ammo == 2)
            {
                return;
            }

            collider.GetComponent<Player>().Data.ammo += 1;
            if (collider.GetComponent<Player>().Data.ammo > 2)
            {
                collider.GetComponent<Player>().Data.ammo = 2;
            }

            onCooldown = true;
            timer = maxTimer;
            GetComponent<MeshRenderer>().enabled = false;

        }

    }

}
