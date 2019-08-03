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

        if(ScoreManager.Instance.teamScores[Team.A] > ScoreManager.Instance.teamScores[Team.One]) {
            text.text = "TEAM A WINS";
        }
        else if (ScoreManager.Instance.teamScores[Team.A] < ScoreManager.Instance.teamScores[Team.One]) {
            text.text = "TEAM ONE WINS";
        }
        else {
            text.text = "EVERYONE LOSES";
        }
    }
}
