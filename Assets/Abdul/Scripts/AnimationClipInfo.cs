using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[Serializable]
public class AnimationClipInfo
{
    public string animationTriggerName;
    public float animationDelayInSeconds = 0f;
    public AudioClip audioClip = null; // Empty means no audio
    public ExecutionPhaseEnum audioExecutionPhase = ExecutionPhaseEnum.SimultaneouslyExecution;
    public float audioDelayInSeconds = 0f; 
    public AnimationClip[] animationsClip; // To allow multiple animations clips to be executed sequentially
    public int animationsClipPointer = 0;

    private bool _isPlaying = false;
    private bool _stopped = false;


    private bool _audioStopped = false;
    private bool _animationStopped = false;

    private Action _callbackWhenDone;

    public AnimationClip GetNextAnimationClip()
    {
        if (animationsClip == null) return null;

        if (animationsClipPointer + 1 >= animationsClip.Length) return null;


        return animationsClip[++animationsClipPointer];
    }

    public AnimationClip GetPreviousAnimationClip()
    {
        if (animationsClip == null) return null;

        if (animationsClipPointer - 1 < 0) return null;


        return animationsClip[--animationsClipPointer];
    }

    public void Play(Animator animator, AudioSource audioSource, Action callbackWhenDone = null)
    {
        _callbackWhenDone = callbackWhenDone;
        _isPlaying = true;
       float animationDelay = animationDelayInSeconds;
        if(audioClip != null)
        {
            float audioClipDelayInSeconds = audioDelayInSeconds;
            float delayAudioInSeconds = audioClipDelayInSeconds;
            switch (audioExecutionPhase)
            {
                case ExecutionPhaseEnum.PreExecution:
                    audioClipDelayInSeconds += audioClip.length;
                    animationDelay += audioClip.length + audioDelayInSeconds;
                    break;

                //case ExecutionPhaseEnum.SimultaneouslyExecution:
                //    // Synchronize the delay time for both the animation and the audio clip
                //    if (audioDelayInSeconds > animationDelayInSeconds) animationDelayInSeconds = audioDelayInSeconds;
                //    else audioDelayInSeconds = animationDelayInSeconds;
                //    break;

                case ExecutionPhaseEnum.PostExecution:
                    delayAudioInSeconds += ExtendedAnimator.GetAnimationDuration(animator, animationTriggerName.Replace("Trigger", ""));
                    break;
            }
            // Play the audio sound
            AudioSourceUtility.PlayDelayedAudio(delayAudioInSeconds, audioSource, audioClip, () => Stop(true));
        }

        ExtendedAnimator.PlayDelayedAnimation(animator, animationTriggerName, animationDelay, () => Stop(false));
    }

    public bool isStopped()
    {
        return _stopped || !_isPlaying;
    }

    private void Stop(bool audio)
    {
        if(audio) _audioStopped = true;
        else _animationStopped = true;

        if(_animationStopped && _audioStopped || (_animationStopped && audioClip == null))
        {
            MonoBehaviour.print("Stopped");
            _stopped = true;
            _isPlaying = false;
            if(_callbackWhenDone != null) _callbackWhenDone();
        }
    }
}
