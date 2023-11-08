using System;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Animateable : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private string _animationTriggerName;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void Animate(float delayInSeconds = 0f, Action callbackWhenDone = null)
    {
        StartCoroutine(_Animate(delayInSeconds, callbackWhenDone));
    }


    private IEnumerator _Animate(float delayInSeconds, Action callbackWhenDone)
    {
        yield return new WaitForSeconds(delayInSeconds);
        _animator.SetTrigger(_animationTriggerName);
        
        if(callbackWhenDone != null) callbackWhenDone();
    }

    public float GetAnimationDuration()
    {
        return ExtendedAnimator.GetAnimationDuration(_animator, _animationTriggerName.Replace("Trigger", ""));
    }
}
