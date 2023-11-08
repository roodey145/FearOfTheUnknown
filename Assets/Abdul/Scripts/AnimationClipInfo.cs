using System;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
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
    public AnimationClipInfo[] animationsClip; // To allow multiple animations clips to be executed sequentially
    public int animationsClipPointer = 0;


    public Animateable animate;
    public ExecutionPhaseEnum animateableExecutionPhase;
    public float animateableDelayInSeconds = 0f;

    //public Action callbackWhenDone = null;
    public StringUnityEvent callWhenDone;

    private bool _isPlaying = false;
    private bool _stopped = false;


    private bool _audioStopped = false;
    private bool _animationStopped = false;

    private Action _callbackWhenDone;

    public AnimationClipInfo GetNextAnimationClip()
    {
        if (animationsClip == null) return null;

        if (animationsClipPointer + 1 >= animationsClip.Length) return null;


        return animationsClip[++animationsClipPointer];
    }

    public AnimationClipInfo GetPreviousAnimationClip()
    {
        if (animationsClip == null) return null;

        if (animationsClipPointer - 1 < 0) return null;


        return animationsClip[--animationsClipPointer];
    }

    public void Play(Animator animator, AudioSource audioSource, Action callbackWhenDone = null)
    {
        // Get the external animateable animation delay 
        float externalAnimationDelay = 0f;
        if (animate != null && animateableExecutionPhase == ExecutionPhaseEnum.PreExecution)
        {
            externalAnimationDelay = animate.GetAnimationDuration();
            animate.Animate(animateableDelayInSeconds);
        }


        _callbackWhenDone = callbackWhenDone;
        _isPlaying = true;
       float animationDelay = animationDelayInSeconds + externalAnimationDelay;
        if(audioClip != null)
        {
            float delayAudioInSeconds = audioDelayInSeconds + externalAnimationDelay;
            switch (audioExecutionPhase)
            {
                case ExecutionPhaseEnum.PreExecution:
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


        // Animate the animateable if it should be animated at the same time with the other animations
        if (animateableExecutionPhase == ExecutionPhaseEnum.SimultaneouslyExecution)
        {
            // Animate an external source
            animate.Animate(animateableDelayInSeconds);
        }
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
            if(animate != null && animateableExecutionPhase == ExecutionPhaseEnum.PostExecution)
            {
                // Animate an external source
                animate.Animate(animateableDelayInSeconds, _callbackWhenDone);
            }
            else if(_callbackWhenDone != null) _callbackWhenDone();
        }
    }
}
