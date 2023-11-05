using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : Event
{
    [SerializeField] private Transform _ancher;
    [SerializeField] private bool _matchProportionToAncherCenter = true;

    protected override void _Action(string activationEvent)
    {
        Vector3 positionToMoveTo = _ancher.position;
        Vector3 extraProportionPosition = _CalcExtraProportionPosition();

        // Get the target character controll if it has one
        CharacterController characterController = _GetTarget().GetComponent<CharacterController>();
        
        // Deactivate the character controller
        if (characterController != null)
        {
            characterController.enabled = false;
        }
        
        _GetTarget().transform.position = positionToMoveTo + extraProportionPosition;
        
        // Reactivate the character controller
        if(characterController != null )
        {
            characterController.enabled = true;
        }
        print("New Position: " + _GetTarget().transform.position);
    }

    private Vector3 _CalcExtraProportionPosition()
    {
        Vector3 extraProportionPosition = Vector3.zero;
        if (_matchProportionToAncherCenter)
        {
            // Calculate the target position in proportion to my teleporter center
            extraProportionPosition += (_GetTarget().transform.position - transform.position);
        }

        return extraProportionPosition;
    }
}
