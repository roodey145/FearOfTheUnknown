using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Listener : MonoBehaviour
{
    [SerializeField] private string _eventName = "EventName";



    // Start is called before the first frame update
    void Awake()
    {
        EventListener eventListener = new EventListener((string eventName) => eventName == _eventName, _Action);
        EventsController.RegisterListener(eventListener);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected abstract void _Action();
}
