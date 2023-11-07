using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightController : MonoBehaviour
{
    public AudioSource source;
    public AudioClip clip;
    public Light spotLight;
    public float minBlinkIntervalOff = 0.1f; // Minimum time between blinks
    public float maxBlinkIntervalOff = 0.2f; // Maximum time between blinks
    public float minBlinkIntervalOn = 1.2f; // Minimum time between blinks
    public float maxBlinkIntervalOn = 8f; // Maximum time between blinks
    public float onTimer;
    public float offTimer;
    float updatedOffValue;
    float updatedOnValue;


    void Start()
    {
        newOffValue();
        newOnValue();
    }

    void Update()
    {
       
        if (spotLight.enabled == false)
        {

            offTimer += Time.deltaTime;
            if (offTimer > updatedOffValue)
            {
                offTimer = 0f;
                newOffValue();
                spotLight.enabled = true;
                source.PlayOneShot(clip);
            }
        }

        if (spotLight.enabled == true)
        {
            onTimer += Time.deltaTime;
            if (onTimer > updatedOnValue)
            {
                onTimer = 0f;
                newOnValue();
                spotLight.enabled = false;
            }
        }

    }
    void newOffValue()
    {
        updatedOffValue = Random.Range(minBlinkIntervalOff, maxBlinkIntervalOff);
    }

    void newOnValue()
    {
        updatedOnValue = Random.Range(minBlinkIntervalOn, maxBlinkIntervalOn);
    }
}