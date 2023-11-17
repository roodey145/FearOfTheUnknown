using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerMovementFreezeListener : Listener
{
    [SerializeField] private bool _freeze = false;
    protected override void _Action()
    {
        GetComponent<ActionBasedContinuousMoveProvider>().moveSpeed = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        if(_freeze) EventsController.RegisterEvent(_eventName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
