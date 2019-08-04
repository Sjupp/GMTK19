using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager INSTANCE = null;

    [SerializeField]
    public Player[] players;
    [SerializeField]
    private Ball ball;

    [SerializeField]
    private InputManager inputManager = null;

    public DateTime matchStartTime;
    [SerializeField]
    public float matchLength = 5400;

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

        if (inputManager == null)
        {
            return;
        }

        inputManager.moveDelegate += players[0].OnMove;
        inputManager.kickDelegate += players[0].OnKick;

    }

    private void Update()
    {
        for (int i = 0; i < players.Length; i++)
        {
            //Debug.Log("Updating player " + i);
            inputManager?.Upd8(i);
            players[i]?.Upd8();
        }

        // end game here
        if(GetTimeLeft().Seconds <= 0 && GetTimeLeft().Minutes <= 0 && GetTimeLeft().Hours <= 0 && InterfaceManager.Instance.InterfaceState == InterfaceState.InGame) {
            InterfaceManager.Instance.UpdateInterfaceState(InterfaceState.GameOver);
        }
    }

    public TimeSpan GetTimeLeft() {
        TimeSpan maxTime = new TimeSpan(0, 0, (int)GameManager.INSTANCE.matchLength);
        //maxTime.A
        TimeSpan deltaDateTime = maxTime - (DateTime.Now - GameManager.INSTANCE.matchStartTime);

        return deltaDateTime;
    }

    public void StartGameRound() 
    {
        // Reset values and positions
        ResetValues();
        ResetInGameObjects();

        // Update interface
        InterfaceManager.Instance.UpdateInterfaceState(InterfaceState.InGame);
    }

    private void ResetValues() 
    {
        ScoreManager.Instance.Reset();

        matchStartTime = DateTime.Now;
    }

    public void ResetInGameObjects() 
    {
        foreach (Player player in players) 
        {
            player.Reset();
        }
        if(ball == null) {
            throw new Exception("GAME MANAGER ERROR: WOULD YOU KINDLY INSERT THE BALL INTO THE VARIABLE SLOT");
        }

        ball.Reset();
    }
}