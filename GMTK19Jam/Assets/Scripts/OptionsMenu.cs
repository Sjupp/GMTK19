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

    [SerializeField] Dropdown matchLength;
    public void SetMatchLength() {
        Debug.Log("Setting match length to " + matchLength.options[matchLength.value].text);

        switch (matchLength.options[matchLength.value].text) {
            case "1 minute":
                GameManager.INSTANCE.matchLength = 1*60;
                break;
            case "2 minutes":
                GameManager.INSTANCE.matchLength = 2*60;
                break;
            case "3 minutes":
                GameManager.INSTANCE.matchLength = 3*60;
                break;
            case "5 minutes":
                GameManager.INSTANCE.matchLength = 5*60;
                break;
            case "9 minutes":
                GameManager.INSTANCE.matchLength = 9*60;
                break;
            case "15 minutes":
                GameManager.INSTANCE.matchLength = 15*60;
                break;
            case "30 minutes":
                GameManager.INSTANCE.matchLength = 30*60;
                break;
            case "45 minutes":
                GameManager.INSTANCE.matchLength = 45*60;
                break;
            case "90 minutes":
                GameManager.INSTANCE.matchLength = 90*60;
                break;
            default:
                GameManager.INSTANCE.matchLength = 60;
                Debug.LogError("OptionsMenu:SetMatchLength error: Invalid matchLength.options[matchLength.value].text");
                break;
        }
    }
}
