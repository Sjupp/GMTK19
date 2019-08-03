using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager INSTANCE = null;

    [SerializeField]
    public Player[] players;

    [SerializeField]
    private InputManager inputManager = null;

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
    }

    public void ResetInGameObjects() 
    {
        foreach (Player player in players) 
        {
            player.Reset();
        }
    }
}