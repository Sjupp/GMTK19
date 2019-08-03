using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public Team myTeam;

    private void OnTriggerEnter(Collider other) 
    {
        // TODO:
        // Check for valid object
        // Maybe a tag or something
        ScoreManager.Instance.UpdateScore(TeamHelper.GetOppositeTeam(myTeam), 1);
    }
}
