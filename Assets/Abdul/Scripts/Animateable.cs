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

    [SerializeField] private string _eventNameToTrigger; // In case 
    [SerializeField] private ExecutionPhaseEnum _eventTriggerPhase; 

    [SerializeField] private Animateable _animateable;
    [SerializeField] private ExecutionPhaseEnum _executionPhase;
    [SerializeField] private float _delayInSeconds;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        // Make sure that the animateable does not repeat it self
        if (_animateable == this) _animateable = null;
    }

    public void Animate(float delayInSeconds = 0f, Action callbackWhenDone = null)
    {
        if(_eventTriggerPhase == ExecutionPhaseEnum.PreExecution ||
            _eventTriggerPhase == ExecutionPhaseEnum.SimultaneouslyExecution)
        {
            EventsController.RegisterEvent(_eventNameToTrigger);
        }

        if(_animateable != null && _executionPhase == ExecutionPhaseEnum.PreExecution)
        {
            _animateable.Animate(delayInSeconds, () => StartCoroutine(_Animate(delayInSeconds, callbackWhenDone)));
            return;
        }
        else if(_animateable != null && _executionPhase == ExecutionPhaseEnum.SimultaneouslyExecution)
        {
            _animateable.Animate(delayInSeconds, null);
        }

        StartCoroutine(_Animate(delayInSeconds, callbackWhenDone));
    }


    private IEnumerator _Animate(float delayInSeconds, Action callbackWhenDone)
    {
        yield return new WaitForSeconds(delayInSeconds);
        _animator.SetTrigger(_animationTriggerName);


        if (_animateable != null && _executionPhase == ExecutionPhaseEnum.PostExecution)
        {
            _animateable.Animate(delayInSeconds, () =>
            {
                if (_eventTriggerPhase == ExecutionPhaseEnum.PostExecution)
                    EventsController.RegisterEvent(_eventNameToTrigger);
                callbackWhenDone();
            });
        }
        else if (callbackWhenDone != null)
        {
            if (_eventTriggerPhase == ExecutionPhaseEnum.PostExecution)
                EventsController.RegisterEvent(_eventNameToTrigger);
            callbackWhenDone();
        }
    }

    public float GetAnimationDuration()
    {
        return ExtendedAnimator.GetAnimationDuration(_animator, _animationTriggerName.Replace("Trigger", ""));
    }
}
