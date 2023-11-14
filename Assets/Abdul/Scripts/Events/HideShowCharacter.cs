using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideShowCharacter : Event
{
    [SerializeField] private string _showEventName = "ShowCharacter";

    // Start is called before the first frame update
    void Start()
    {
        EventListener eventListener = new EventListener((string eventName) => eventName == _showEventName, _ShowCharacter);
        EventsController.RegisterListener( eventListener );
    }

    protected override void _Action(string activationEvent)
    {
        _GetTarget().gameObject.SetActive(false); // Hide the character
    }


    private void _ShowCharacter()
    {
        _GetTarget().gameObject.SetActive(true);
    }
}
