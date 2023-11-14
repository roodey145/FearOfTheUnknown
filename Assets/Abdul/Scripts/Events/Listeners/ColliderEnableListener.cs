using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderEnableListener : MonoBehaviour
{
    [SerializeField] private string _eventName = "EnableCollider";
    [SerializeField] private Collider _gameObject;

    private void Start()
    {
        EventListener listener = new EventListener( (string eventName) => eventName == _eventName, _Enable );
        EventsController.RegisterListener( listener );
    }

    private void _Enable()
    {
        _gameObject.enabled = true;
    }
}
