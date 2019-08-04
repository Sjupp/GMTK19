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
            text.text = "TEAM P2 WINS";
        }
        else if (ScoreManager.Instance.teamScores[Team.P2] < ScoreManager.Instance.teamScores[Team.P1]) {
            text.text = "TEAM P1 WINS";
        }
        else {
            text.text = "EVERYONE LOSES";
        }

        text.text += "\n THE FINAL RESULT WAS \nP1  P2\n" + ScoreManager.Instance.teamScores[Team.P1] + "   " + ScoreManager.Instance.teamScores[Team.P2];
    }
}
