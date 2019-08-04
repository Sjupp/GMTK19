using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{

    public static CameraShake INSTANCE = null;

    public Camera camera;

    private void Awake()
    {

        if (!INSTANCE)
        {
            INSTANCE = this;
            return;
        }

        Destroy(gameObject);

    }

    private void Start()
    {
        startPos = camera.transform.position;
    }

    Vector3 startPos;

    public void Shake(float shakeAmount = 5, float shakeTime = 2)
    {
        this.shakeAmount = shakeAmount;
        maxShakingTimer = shakeTime;
        shakingTimer = maxShakingTimer;
        shaking = true;
    }

    bool shaking = false;
    float maxShakingTimer = 1f;
    float shakingTimer = 0f;

    public float shakeAmount = 5;

    bool resetting = false;

    private void Update()
    {

        if (shaking)
        {

            camera.transform.position += new Vector3(Random.Range(-1f, 1f), Random.Range(-1, 1f), Random.Range(-1, 1f)) * Time.deltaTime * shakeAmount;

            shakingTimer -= Time.deltaTime;

            if (shakingTimer <= 0)
            {
                shaking = false;
                resetting = true;
            }

        }

        if (resetting)
        {
            camera.transform.position = Vector3.Lerp(camera.transform.position, startPos, 0.25f);

            if (Vector3.Distance(camera.transform.position, startPos) < 0.1f)
            {
                resetting = false;
            }

        }

    }

}
