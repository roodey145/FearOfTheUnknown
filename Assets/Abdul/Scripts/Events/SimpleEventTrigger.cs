using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEventTrigger : MonoBehaviour
{
    [SerializeField] private string _eventName;
    
    public void TriggerEvent()
    {
        EventsController.RegisterEvent( _eventName );
    }
}
