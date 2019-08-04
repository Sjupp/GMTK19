using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioInitializer : MonoBehaviour
{
    [SerializeField] private bool muteSound;

    // Start is called before the first frame update
    void Start()
    {
        ServiceLocator.Initialize();
        if(muteSound)
        {
            ServiceLocator.ProvideAudio(new NullAudioProvider());
        }
        else
        {
            ServiceLocator.ProvideAudio(new NewAudioProvider());
            ServiceLocator.GetAudio().LoadSounds();
        }
        
        //ServiceLocator.GetAudio().PlaySound("Music_Gameplay01");
        //ServiceLocator.GetAudio().PlaySound("VO_ReadyGo");
    }

    // For testing
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    ServiceLocator.GetAudio().PlaySound("Explosion");
        //}
    }
}
