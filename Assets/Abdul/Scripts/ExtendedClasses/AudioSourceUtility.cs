using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceUtility
{

    public static void PlayDelayedAudio(float delayInSeconds, AudioSource audioSource, AudioClip audioClip, System.Action StopAudio)
    {
        if(GuideCharacterController.instance.isActiveAndEnabled)
            GuideCharacterController.instance.StartCoroutine(_PlayDelayedAudio(delayInSeconds, audioSource, audioClip, StopAudio));
    }

    private static IEnumerator _PlayDelayedAudio(float delayInSeconds, AudioSource audioSource, AudioClip audioClip, System.Action StopAudio)
    {
        yield return new WaitForSeconds(delayInSeconds);

        audioSource.clip = audioClip;
        audioSource.Play();

        yield return new WaitForSeconds(audioClip.length);

        StopAudio();
    }
}
