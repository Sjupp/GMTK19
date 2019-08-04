using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Legspawner : MonoBehaviour
{

    private Collider coll;

    public GameObject legObj;

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

                legObj.SetActive(true);
            }

        }

    }

    private void OnTriggerEnter(Collider collider)
    {

        if (collider.CompareTag("Player"))
        {
            if (onCooldown)
            {
                return;
            }

            if (collider.GetComponent<Player>().Data.ammo == 2)
            {
                return;
            }

            collider.GetComponent<Player>().Data.ammo = 2;

            legObj.SetActive(false);
            timer = maxTimer;
            onCooldown = true;
        }

    }

}
