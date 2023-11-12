using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;
using UnityEngine.Experimental.GlobalIllumination;

public class constructionLight : MonoBehaviour
{
    public static constructionLight instance;
    public AudioSource source;
    public AudioClip clip;
    public Light spotLight;
    public float minBlinkIntervalOff = 0.1f; // Minimum time between blinks
    public float maxBlinkIntervalOff = 0.2f; // Maximum time between blinks
    public float minBlinkIntervalOn = 1.2f; // Minimum time between blinks
    public float maxBlinkIntervalOn = 8f; // Maximum time between blinks
    float onTimer;
    float offTimer;
    float updatedOffValue;
    float updatedOnValue;
    public Material material;
    public bool startBlink = false;


    void Start()
    {
        instance = this;
        newOffValue();
        newOnValue();
    }

    void Update()
    {
        if (startBlink) 
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

                    if (material != null)
                    {
                        material.SetColor("_EmissionColor", new Color(0.56f, 0.75f, 0.29f, 1.0f));

                    }
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
                    if (material != null)
                    {
                        material.SetColor("_EmissionColor", Color.black);

                    }
                }
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
