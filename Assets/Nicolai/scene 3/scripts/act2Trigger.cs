using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class act2Trigger : MonoBehaviour
{
    public float timer;
    private bool actComplete;
    public Light spotLight;
    public AudioSource[] source;
    public AudioClip[] clip;
    public GameObject girl;
    public Rigidbody portrait;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (actComplete)
        {
            timer += Time.deltaTime;

            float shodowValue = Mathf.Lerp(1f, 0.5f, timer / 1.5f);
            spotLight.shadowStrength = shodowValue;

            if (timer > 1.5f)
            {
                float rangeValue = Mathf.Lerp(5.5f, 0f, (timer - 1.5f) / 0.5f);
                


                // Set the light's range to the calculated lerp value
                spotLight.range = rangeValue;
                girl.SetActive(false);
                portrait.useGravity = true;
                //constructionLight.instance.startBlink = true;
            }

        }

        

        //move girl
        // change light
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !actComplete)
        {
            
            source[0].PlayOneShot(clip[0]);
            actComplete = true;

        }
    }
}
