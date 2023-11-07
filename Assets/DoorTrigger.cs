using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class DoorTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource[] source;
    public AudioClip[] clip;
    public float timer = 0f;
    public Rigidbody paintingRB;

    private bool startSequence;
    private bool audio0C = false;
    private bool audio1C = false;
    private bool audio2C = false;



    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (startSequence) { 
        timer += Time.deltaTime;

            //play door sound
            if (!audio0C)
            {
                source[0].PlayOneShot(clip[0]);
                audio0C = true;
            }

        if (timer > 3 && !audio1C)
        {
            source[1].PlayOneShot(clip[1]);
                audio1C = true;
            }

        if (timer > 6 && !audio2C)
        {
                //play pinting falling sound and animation
                paintingRB.useGravity = true;
                source[2].PlayOneShot(clip[2]);
                audio2C = true;
        }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            startSequence = true;
        }

        if(other.tag == "Key")
        {
            source[3].PlayOneShot(clip[3]);
            Debug.Log("YOOO");
            //
            //fade to new scene
        }
    }
}
