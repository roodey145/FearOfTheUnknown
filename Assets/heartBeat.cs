using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heartBeat : MonoBehaviour
{
    public static heartBeat instance;
    public bool heartBeating = false;
    public bool hasStarted = false;
    public AudioSource source;
    public AudioClip clip;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (heartBeating == true) {
            PlayHeartbeat();
        }
        

    }

    void PlayHeartbeat()
    {
        if (hasStarted == false)
        {
            source.PlayOneShot(clip);
            hasStarted = true;
        }
    }
}
