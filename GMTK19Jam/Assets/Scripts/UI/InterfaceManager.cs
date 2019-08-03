using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InterfaceState { Initial, MainMenu, InGame };
public class InterfaceManager : Singleton<InterfaceManager> {

    public delegate void EnableInterface();
    public delegate void DisableInterface();

    public Dictionary<InterfaceState, EnableInterface> OnEnableInterfaceWithState = new Dictionary<InterfaceState, EnableInterface>();
    public Dictionary<InterfaceState, EnableInterface> OnDisableInterfaceWithState = new Dictionary<InterfaceState, EnableInterface>();

    InterfaceState interfaceState = InterfaceState.Initial;
    public InterfaceState InterfaceState 
    {
        get { return interfaceState; }
        private set { interfaceState = value; }
    }

    void OnEnable() 
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
        OnEnableInterfaceWithState[InterfaceState.InGame] = new EnableInterface(EnableInterfaceWithState);

        OnDisableInterfaceWithState[InterfaceState.Initial] = new EnableInterface(DisableInterfaceWithState);
        OnDisableInterfaceWithState[InterfaceState.MainMenu] = new EnableInterface(DisableInterfaceWithState);
        OnDisableInterfaceWithState[InterfaceState.InGame] = new EnableInterface(DisableInterfaceWithState);
    }

    /// <summary>
    /// Sets the interface state to the InterFace state matching the int.
    /// </summary>
    /// <param name="newState"></param>
    public void UpdateInterfaceState(int newState) { UpdateInterfaceState((InterfaceState)newState); }
    /// <summary>
    /// Updates the interfaceState and invokes 
    /// OnDisableInterfaceWithState on the old state and 
    /// OnEnableInterfaceWithState on the new
    /// </summary>
    /// <param name="newState"></param>
    void UpdateInterfaceState(InterfaceState newState) 
    {
        try 
        {
            OnDisableInterfaceWithState[interfaceState]?.Invoke();
        }
        catch(System.Exception e) 
    {
            Debug.Log(e);
        }
        interfaceState = newState;

        OnEnableInterfaceWithState[newState]?.Invoke();
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
}
