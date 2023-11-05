using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public abstract class Event : MonoBehaviour
{
    [SerializeField] protected string _targetTag = "Player";
    [SerializeField] protected string _eventName = "Stage_2";
    [SerializeField] protected bool _repeatable = false;
    [SerializeField] private bool _hasBeenExecuted = false;
    private GameObject _currentTarget;

    private void OnTriggerEnter(Collider other)
    {
        print(other.name);
        if(other.CompareTag(_targetTag))
        {
            print("Player Entered");
            if(!_hasBeenExecuted || _repeatable)
            {
                _currentTarget = other.gameObject;
                _TriggerEvent();
                _Action(_eventName);
                _hasBeenExecuted = true;
            }
            
        }
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
