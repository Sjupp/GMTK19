using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIElement : MonoBehaviour
{
    [Tooltip("" +
        "0:Initial" +
        "1:MainMenu" +
        "2:OptionsMenu" +
        "3:InGame" +
        "4:InGameMenu")]
    public InterfaceState[] interfaceStates;
    [SerializeField] bool initialized = false;

    private void Awake() 
    {
        if (!initialized) {
            initialized = true;

            Debug.Log("Enabling " + name + " state: " + interfaceStates);

            foreach(InterfaceState interfaceState in interfaceStates) 
            {
                InterfaceManager.Instance.OnEnableInterfaceWithState[interfaceState] += Enable;
                InterfaceManager.Instance.OnDisableInterfaceWithState[interfaceState] += Disable;
            }
            
            Disable();
        }
    }

    void Enable() 
    {
        gameObject.SetActive(true);
    }

    void Disable() 
    {
        gameObject.SetActive(false);
    }
}
