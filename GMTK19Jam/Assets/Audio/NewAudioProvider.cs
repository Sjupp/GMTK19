using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewAudioProvider : IAudioService
{

    private GameObject audioSourceContainer;

    //Player
    private AudioSource[] kickBallAudioSource;
    private AudioSource[] kickPlayerAudioSource;
    private AudioSource[] projectileAudioSource;
    private AudioSource[] projectileExplosionAudioSource;
    private AudioSource[] walkAudioSource;

    //VO
    private AudioSource[] goalAudioSource;
    private AudioSource[] readyGoAudioSource;
    private AudioSource[] publicCheerAudioSource;

    //UI
    private AudioSource uiSelectAudioSource;

    //Misc
    private AudioSource[] ExplosionAudioSource;

    //Music
    private AudioSource musicGameplay01AudioSource;

    public void LoadSounds()
    {
        audioSourceContainer = GameObject.FindGameObjectWithTag("AudioSources");

        kickBallAudioSource = InstantiateAudioSources(kickBallAudioSource, "Player_KickBall");
        kickPlayerAudioSource = InstantiateAudioSources(kickPlayerAudioSource, "Player_KickPlayer");
        projectileAudioSource = InstantiateAudioSources(projectileAudioSource, "Player_Projectile");
        projectileExplosionAudioSource = InstantiateAudioSources(projectileExplosionAudioSource, "Player_ProjectileExplosion");
        walkAudioSource = InstantiateAudioSources(walkAudioSource, "Player_Walk");


        goalAudioSource = InstantiateAudioSources(goalAudioSource, "VO_Goal");
        readyGoAudioSource = InstantiateAudioSources(readyGoAudioSource, "VO_ReadyGo");
        publicCheerAudioSource = InstantiateAudioSources(publicCheerAudioSource, "VO_PublicCheer");

        ExplosionAudioSource = InstantiateAudioSources(ExplosionAudioSource, "Explosion");

        uiSelectAudioSource = instantiateSingleAudioSource(uiSelectAudioSource, "UI_Select");



        musicGameplay01AudioSource = instantiateSingleAudioSource(musicGameplay01AudioSource, "Music_Gameplay01");
    }

    private AudioSource instantiateSingleAudioSource(AudioSource audioSource, string soundID)
    {
        Transform tempTransform = audioSourceContainer.transform.Find(soundID);
        audioSource = tempTransform.GetComponent<AudioSource>();
        return audioSource;
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
                break;

            case "Player_ProjectileExplosion":
                PlaySoundFromArray(projectileExplosionAudioSource);
                break;

            case "Player_Walk":
                PlaySoundFromArray(walkAudioSource);
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

            case "UI_Select":
                uiSelectAudioSource.Play();
                break;


            case "Music_Gameplay01":
                musicGameplay01AudioSource.Play();
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

    }

    
}
