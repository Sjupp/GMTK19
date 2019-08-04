using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] Toggle fullscreenToggle;
    public void ToggleFullscreen() 
    {
        Screen.fullScreen = fullscreenToggle.isOn; 
    }
}
