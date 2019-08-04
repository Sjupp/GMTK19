using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewAudioProvider : MonoBehaviour, IAudioService
{

    private GameObject audioSourceContainer;

    //Player
    private AudioSource[] kickBallAudioSource;
    private AudioSource[] kickPlayerAudioSource;
    private AudioSource[] projectileAudioSource;
    private AudioSource[] projectileFizzleAudioSource;
    private AudioSource[] projectileExplosionAudioSource;
    private AudioSource[] walkAudioSource;
    private AudioSource[] noAmmoAudioSource;

    //VO
    private AudioSource[] goalAudioSource;
    private AudioSource[] readyGoAudioSource;
    private AudioSource[] publicCheerAudioSource;

    //UI
    private AudioSource uiSelectAudioSource;

    //Misc
    private AudioSource[] ExplosionAudioSource;
    private AudioSource[] PickupAudioSource;

    //Music
    private AudioSource musicIntroAudioSource;
    private AudioSource musicLoopAudioSource;
    private LoopStarter loopStarter;

    public void LoadSounds()
    {
        audioSourceContainer = GameObject.FindGameObjectWithTag("AudioSources");

        kickBallAudioSource = InstantiateAudioSources(kickBallAudioSource, "Player_KickBall");
        kickPlayerAudioSource = InstantiateAudioSources(kickPlayerAudioSource, "Player_KickPlayer");
        projectileAudioSource = InstantiateAudioSources(projectileAudioSource, "Player_Projectile");
        projectileFizzleAudioSource = InstantiateAudioSources(projectileFizzleAudioSource, "Player_ProjectileFizzle");
        projectileExplosionAudioSource = InstantiateAudioSources(projectileExplosionAudioSource, "Player_ProjectileExplosion");
        walkAudioSource = InstantiateAudioSources(walkAudioSource, "Player_Walk");
        noAmmoAudioSource = InstantiateAudioSources(noAmmoAudioSource, "Player_NoAmmo");
        PickupAudioSource = InstantiateAudioSources(PickupAudioSource, "Pickup");


        goalAudioSource = InstantiateAudioSources(goalAudioSource, "VO_Goal");
        readyGoAudioSource = InstantiateAudioSources(readyGoAudioSource, "VO_ReadyGo");
        publicCheerAudioSource = InstantiateAudioSources(publicCheerAudioSource, "VO_PublicCheer");

        ExplosionAudioSource = InstantiateAudioSources(ExplosionAudioSource, "Explosion");

        uiSelectAudioSource = instantiateSingleAudioSource(uiSelectAudioSource, "UI_Select");



        musicIntroAudioSource = instantiateSingleAudioSource(musicIntroAudioSource, "Music_Intro");
        musicLoopAudioSource = instantiateSingleAudioSource(musicLoopAudioSource, "Music_Loop");
        loopStarter = getLoopStarter();
    }

    private AudioSource instantiateSingleAudioSource(AudioSource audioSource, string soundID)
    {
        Transform tempTransform = audioSourceContainer.transform.Find(soundID);
        audioSource = tempTransform.GetComponent<AudioSource>();
        return audioSource;
    }

    private LoopStarter getLoopStarter()
    {
        return musicLoopAudioSource.GetComponent<LoopStarter>();
    }

    private AudioSource[] InstantiateAudioSources(AudioSource[] audioSource, string soundID)
    {
        audioSource = new AudioSource[CountAudioSources(soundID)];

        for (int i = 0; i < CountAudioSources(soundID); i++)
        {
            Transform tempTransform;

            tempTransform = audioSourceContainer.transform.Find(soundID + i);

            if (tempTransform != null)
            {
                audioSource[i] = tempTransform.GetComponent<AudioSource>();
            }
        }
        return audioSource;
    }

    private int CountAudioSources(string soundID)
    {
        int numberOfSounds = 0;
        while (audioSourceContainer.transform.Find(soundID + numberOfSounds) != null)
        {
            numberOfSounds++;
        }
        return numberOfSounds;
    }

    public void PlaySound(string soundID)
    {
        switch (soundID)
        {
            case "Player_KickBall":
                PlaySoundFromArray(kickBallAudioSource);
                break;

            case "Player_KickPlayer":
                PlaySoundFromArray(kickPlayerAudioSource);
                break;

            case "Player_Projectile":
                PlaySoundFromArray(projectileAudioSource);
                PlaySoundFromArray(projectileFizzleAudioSource);
                break;

            case "Player_ProjectileExplosion":
                PlaySoundFromArray(projectileExplosionAudioSource);
                break;

            case "Player_Walk":
                PlaySoundFromArray(walkAudioSource);
                break;

            case "Player_NoAmmo":
                PlaySoundFromArray(noAmmoAudioSource);
                break;

            case "VO_Goal":
                PlaySoundFromArray(goalAudioSource);
                break;

            case "VO_ReadyGo":
                PlaySoundFromArray(readyGoAudioSource);
                break;

            case "VO_PublicCheer":
                PlaySoundFromArray(publicCheerAudioSource);
                break;

            case "Explosion":
                PlaySoundFromArray(ExplosionAudioSource);
                PlaySoundFromArray(kickBallAudioSource);
                break;

            case "Pickup":
                PlaySoundFromArray(PickupAudioSource);
                break;

            case "UI_Select":
                uiSelectAudioSource.Play();
                break;


            case "Music_Gameplay01":
                musicIntroAudioSource.Play();
                loopStarter.StartDelayedLoop();
                break;

            case "Music_Loop":
                musicLoopAudioSource.Play();
                break;

            default:
                Debug.Log("Sound: " + soundID + " dosen't exist");
                break;
        }

    }


    private void PlaySoundFromArray(AudioSource[] audioSourceToPlay)
    {
        if(audioSourceToPlay.Length != 0)
        {
            if (audioSourceToPlay.Length > 1)
            {
                audioSourceToPlay[Random.Range(0, audioSourceToPlay.Length)].Play();
            }
            else
            {
                audioSourceToPlay[0].Play();
            }
        }
    }

    public void StopAll()
    {

    }

    public void StopSound(string soundID)
    {
        switch (soundID)
        {
            case "Music_Gameplay01" :
                musicLoopAudioSource.Stop();
                musicIntroAudioSource.Stop();
                break;
        }
    }

    
}
