using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class PlayerScore : MonoBehaviour
{
    Text text;

    public Team team;

    void Start()
    {
        text = GetComponent<Text>();
    }

    private void OnEnable() 
    {
        ScoreManager.Instance.OnScoreChanged += UpdateScoreText;   
    }

    private void OnDisable() 
    {
        ScoreManager.Instance.OnScoreChanged -= UpdateScoreText;
    }

    void UpdateScoreText(Team team, float delta, float newScore) 
    {
        if(this.team == team) 
        {
            text.text = team + " score: " + newScore;
        }
    }
}
