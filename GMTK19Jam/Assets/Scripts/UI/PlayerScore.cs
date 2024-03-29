﻿using System.Collections;
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

        // Initialize the text string with the most up to date value
        UpdateScoreText(team, 0, ScoreManager.Instance.teamScores[team]);
    }

    private void OnDisable() 
    {
        if (ScoreManager.Instance != null)
            ScoreManager.Instance.OnScoreChanged -= UpdateScoreText;
    }

    void UpdateScoreText(Team team, float delta, float newScore) 
    {
        if(this.team == team) 
        {
            if(text == null) 
            {
                text = GetComponent<Text>();
            }

            text.text = team + " score: " + newScore;
        }
    }
}
