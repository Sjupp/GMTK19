using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITimer : MonoBehaviour
{
    Text text;

    private void Start() {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        TimeSpan deltaDateTime = GameManager.INSTANCE.GetTimeLeft();

        int seconds = deltaDateTime.Seconds;
        int minutes = deltaDateTime.Minutes;
        int hours = deltaDateTime.Hours;

        string s = string.Format("{0:00}::{1:00}::{2:00}", hours, minutes, seconds);

        text.text = s;
    }
}
