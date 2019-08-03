using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIElement : MonoBehaviour
{
    [Tooltip("The part of the interface this element belongs to.")]
    public InterfaceState interfaceState = InterfaceState.MainMenu;
    bool initialized = false;

    private void OnEnable() 
    {
        InterfaceManager.Instance.OnEnableInterfaceWithState[interfaceState] += Enable;
        InterfaceManager.Instance.OnDisableInterfaceWithState[interfaceState] += Disable;

        if (!initialized) 
        {
            initialized = true;
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
