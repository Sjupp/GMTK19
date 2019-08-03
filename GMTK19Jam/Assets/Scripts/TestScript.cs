using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestScript : MonoBehaviour
{
    public GameObject ball;
    public GameObject projectile;

    public float force = 1.0f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            projectile.GetComponent<Rigidbody>().velocity = Vector3.left * force;
        }
    }

}
