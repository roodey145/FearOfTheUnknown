using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public abstract class Event : MonoBehaviour
{
    [SerializeField] protected string _targetTag = "Player";
    [SerializeField] protected string _eventName = "Stage_2";
    [SerializeField] protected float _passiveEventTriggerDelay = 0;
    [SerializeField] protected bool _repeatable = false;
    [SerializeField] private bool _hasBeenExecuted = false;
    private GameObject _currentTarget;

    private void OnTriggerEnter(Collider other)
    {
        //print(other.name);
        if(other.CompareTag(_targetTag))
        {
            if(!_hasBeenExecuted || _repeatable)
            {
                _currentTarget = other.gameObject;

                StartCoroutine(_DelayedEventTrigger(_passiveEventTriggerDelay));
                
                _hasBeenExecuted = true;
            }
            
        }
    }

    private IEnumerator _DelayedEventTrigger(float delay)
    {
        yield return new WaitForSeconds(delay);

        _TriggerEvent();
        _Action(_eventName);
    }

    protected GameObject _GetTarget()
    {
        return _currentTarget;
    }

    private void _TriggerEvent()
    {
        EventsController.RegisterEvent(_eventName);
    }

    protected abstract void _Action(string activationEvent);
}
