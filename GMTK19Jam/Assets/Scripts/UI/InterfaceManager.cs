using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InterfaceState { Initial, MainMenu, OptionsMenu, InGame, InGameMenu, GameOver};
public class InterfaceManager : Singleton<InterfaceManager> {

    public delegate void EnableInterface();
    public delegate void DisableInterface();

    public Dictionary<InterfaceState, EnableInterface> OnEnableInterfaceWithState = new Dictionary<InterfaceState, EnableInterface>();
    public Dictionary<InterfaceState, EnableInterface> OnDisableInterfaceWithState = new Dictionary<InterfaceState, EnableInterface>();

    [SerializeField] InterfaceState interfaceState = InterfaceState.Initial;
    public InterfaceState InterfaceState 
    {
        get { return interfaceState; }
        private set { interfaceState = value; }
    }

    void Awake() 
    {
        InitializeInterfaceStates();
    }

    private void Start() 
    {
        // Activate all relevant interfaces
        UpdateInterfaceState(interfaceState);
    }

    void InitializeInterfaceStates() 
    {
        OnEnableInterfaceWithState[InterfaceState.Initial] = new EnableInterface(EnableInterfaceWithState);
        OnEnableInterfaceWithState[InterfaceState.MainMenu] = new EnableInterface(EnableInterfaceWithState);
        OnEnableInterfaceWithState[InterfaceState.OptionsMenu] = new EnableInterface(EnableInterfaceWithState);
        OnEnableInterfaceWithState[InterfaceState.InGame] = new EnableInterface(EnableInterfaceWithState);
        OnEnableInterfaceWithState[InterfaceState.InGameMenu] = new EnableInterface(EnableInterfaceWithState);
        OnEnableInterfaceWithState[InterfaceState.GameOver] = new EnableInterface(EnableInterfaceWithState);

        OnDisableInterfaceWithState[InterfaceState.Initial] = new EnableInterface(DisableInterfaceWithState);
        OnDisableInterfaceWithState[InterfaceState.MainMenu] = new EnableInterface(DisableInterfaceWithState);
        OnDisableInterfaceWithState[InterfaceState.OptionsMenu] = new EnableInterface(DisableInterfaceWithState);
        OnDisableInterfaceWithState[InterfaceState.InGame] = new EnableInterface(DisableInterfaceWithState);
        OnDisableInterfaceWithState[InterfaceState.InGameMenu] = new EnableInterface(DisableInterfaceWithState);
        OnDisableInterfaceWithState[InterfaceState.GameOver] = new EnableInterface(DisableInterfaceWithState);
    }

    /// <summary>
    /// Sets the interface state to the InterFace state matching the int.
    /// </summary>
    /// <param name="newState"></param>
    public void UpdateInterfaceState(int newState) 
    {
        if(newState == 3) {
            Debug.LogWarning("You are setting the gameState value to 3 through the int variant " +
                             "but instead you should be calling GameManager.StartGameRound " +
                             "are you sure this is intentional?");
        }
        UpdateInterfaceState((InterfaceState)newState);
    }
    /// <summary>
    /// Updates the interfaceState and invokes 
    /// OnDisableInterfaceWithState on the old state and 
    /// OnEnableInterfaceWithState on the new
    /// </summary>
    /// <param name="newState"></param>
    public void UpdateInterfaceState(InterfaceState newState) 
    {
        try 
        {
            OnDisableInterfaceWithState[interfaceState]?.Invoke();
        }
        catch(System.Exception e) 
        {
            Debug.Log(e);
        }

        SoundStarter(newState);

        interfaceState = newState;

        // Successfully changed state

        OnEnableInterfaceWithState[newState]?.Invoke();
    }


    private void SoundStarter(InterfaceState newState)
    {
        if (interfaceState != InterfaceState.Initial)
        {
            ServiceLocator.GetAudio().PlaySound("UI_Select");
        }

        if (newState == InterfaceState.InGame && interfaceState != InterfaceState.GameOver)
        {
            ServiceLocator.GetAudio().PlaySound("Music_Gameplay01");
            ServiceLocator.GetAudio().PlaySound("VO_ReadyGo");
        }

        if (interfaceState == InterfaceState.GameOver && newState == InterfaceState.MainMenu)
        {
            ServiceLocator.GetAudio().StopSound("Music_Gameplay01");
        }

        if(InterfaceState == InterfaceState.InGame && newState == InterfaceState.GameOver)
        {
            ServiceLocator.GetAudio().PlaySound("VO_PublicCheer");
        }
        //When you go from ingame to game over, applause

        //When you 

    }

    /// <summary>
    /// Dummy function needed to initialize the dictionary elements
    /// </summary>
    void EnableInterfaceWithState() 
    {
        //Debug.Log("Enabling " + interfaceState);
    }

    /// <summary>
    /// Dummy function needed to initialize the dictionary elements
    /// </summary>
    void DisableInterfaceWithState() 
    {
        //Debug.Log("Disabling " + interfaceState);
    }

    public void QuitGame() 
    {
        Application.Quit();
    }
}
