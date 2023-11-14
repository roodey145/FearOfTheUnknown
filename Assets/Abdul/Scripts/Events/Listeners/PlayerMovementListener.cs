using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerMovementListener : Listener
{
    [SerializeField] private float _movementSpeed = 1f;
    protected override void _Action()
    {
        GetComponent<ActionBasedContinuousMoveProvider>().moveSpeed = _movementSpeed;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
