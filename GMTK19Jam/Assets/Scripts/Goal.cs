using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public Team myTeam;

    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Ball")) 
        {
            ScoreManager.Instance.UpdateScore(TeamHelper.GetOppositeTeam(myTeam), 1);
            GameManager.INSTANCE.ResetInGameObjects();
        }
    }
}
