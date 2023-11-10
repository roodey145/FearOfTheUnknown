using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DestinationInfo
{
    public string name = "Destination"; // For Easier readability in the unity window
    public Transform destination;
    public bool matchDestinationRotation = false;
    public AnimationClipInfo clip; // The animations which will be executed when we reach the destination
    public string movingAnimationTriggerName = "WalkTrigger"; // The name of the trigger animation when moving toward destination
    public float speedIncrease = 0f; // Used to increase the movement speed 
}
