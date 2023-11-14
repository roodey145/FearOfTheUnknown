using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portraitDrop : MonoBehaviour
{
    public AudioSource source;
    public AudioClip clip;
    public Rigidbody portrait;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        portrait = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "hands")
        {
            portrait.useGravity = true;
            source.PlayOneShot(clip);

        }
    }
}
