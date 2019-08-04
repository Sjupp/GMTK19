using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class GameOverUI : MonoBehaviour
{
    Text text;
    void Start()
    {
        text = GetComponent<Text>();

        if(ScoreManager.Instance.teamScores[Team.P2] > ScoreManager.Instance.teamScores[Team.P1]) {
            text.text = "TEAM A WINS";
        }
        else if (ScoreManager.Instance.teamScores[Team.P2] < ScoreManager.Instance.teamScores[Team.P1]) {
            text.text = "TEAM ONE WINS";
        }
        else {
            text.text = "EVERYONE LOSES";
        }
    }
}
