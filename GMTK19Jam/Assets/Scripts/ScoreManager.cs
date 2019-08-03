using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Team { One, A };
public class ScoreManager : Singleton<ScoreManager>
{
    public Dictionary<Team, float> teamScores = new Dictionary<Team, float> { { Team.One, 0 }, { Team.A, 0 } };

    public delegate void ScoreChangedDelegate(Team team, float delta, float newScore);
    public event ScoreChangedDelegate OnScoreChanged;

    public void UpdateScore(Team team, float delta) 
    {
        try 
        {
            float oldScore = teamScores[team];
        }
        catch(System.Exception e) 
        {
            Debug.LogError("ScoreManager UpdateScore error: " + e.Message);
            return;
        }

        float newScore = teamScores[team] += delta;
        OnScoreChanged?.Invoke(team, delta, newScore);
    }

    public void Reset() {
        //teamScores[Team.A] = 0;
        //teamScores[Team.One] = 0;

        UpdateScore(Team.One, -teamScores[Team.One]);
        UpdateScore(Team.A, -teamScores[Team.A]);
    }

    //private void Update() 
    //{
    //    //// FOR DEBUG PURPOSES ONLY
    //    //if (Input.GetKeyDown(KeyCode.Alpha1)) {
    //    //    UpdateScore(Team.One, 1);
    //    //}
    //    //else if (Input.GetKeyDown(KeyCode.A)) {
    //    //    UpdateScore(Team.A, 1);
    //    //}
    //}
}
