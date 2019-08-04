using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetAll : MonoBehaviour
{

    public void OnTriggerEnter(Collider collider)
    {

        if (collider.CompareTag("Player") || collider.CompareTag("Ball"))
        {
            GameManager.INSTANCE.ResetInGameObjects();
        }
        
    }

}
