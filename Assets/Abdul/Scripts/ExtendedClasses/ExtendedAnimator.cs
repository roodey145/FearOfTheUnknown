using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtendedAnimator
{
    private static bool _isPlaying = false;

    /// <summary>
    /// Gets the duration of a specific animations using its name. Case insensitive.
    /// </summary>
    /// <param name="animationClipName">The name of the animation</param>
    /// <returns>The duration of the animation or throw an error if the animation was not found.</returns>
    public static float GetAnimationDuration(Animator animator, string animationClipName)
    {
        float duration = -1f;
        AnimationClip[] animations = animator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip anim in animations)
        {
            if (anim.name.ToLower() == animationClipName.ToLower())
            {
                duration = anim.length;
                break;
            }
            else MonoBehaviour.print("Animation Name: " + anim.name);
        }

        if(duration < 0f)
        {
            throw new System.Exception($"No animation with the name({animationClipName}) was found when trying to find the duration of the animation!");
        }

        return duration;
    }


    public static void PlayDelayedAnimation(Animator animator, string animationTriggerName, float delayInSeconds, System.Action StopAnimation)
    {

        // Get the animation duration
        float animationDuration = GetAnimationDuration(animator, animationTriggerName.Replace("Trigger", ""));
        GuideCharacterController.instance.StartCoroutine(_PlayDelayedAnimation(animationTriggerName, delayInSeconds, animationDuration, animator.SetTrigger, StopAnimation));
    }


    private static IEnumerator _PlayDelayedAnimation(string animationTriggerName, float delayInSeconds, float animationDuration, System.Action<string> SetTrigger, System.Action StopAnimation)
    {
        _isPlaying = true;
        yield return new WaitForSeconds(delayInSeconds);

        // Play the animation
        SetTrigger(animationTriggerName);

        // Wait until the animation ends
        yield return new WaitForSeconds(animationDuration);
        
        _isPlaying = false;

        StopAnimation();
    }

    public static bool IsPlaying() { return _isPlaying; }
}
