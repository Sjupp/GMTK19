using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopStarter : MonoBehaviour
{
    [SerializeField] private AudioClip introClip;

    public void StartDelayedLoop()
    {
        StartCoroutine(DelayedLoop());
    }

    private IEnumerator DelayedLoop()
    {
        yield return new WaitForSeconds(introClip.length);
        ServiceLocator.GetAudio().PlaySound("Music_Loop");
        yield return null;
    }
}
