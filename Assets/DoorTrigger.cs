using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;

public class DoorTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource[] source;
    public AudioClip[] clip;
    public float timer = 0f;
    public Rigidbody paintingRB;
    [SerializeField] private string _sceneEndedEventName = "SceneEnded";

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
        //if (startSequence) 
        //{ 
        //    timer += Time.deltaTime;

        //    //play door sound
        //    if (!audio0C)
        //    {
        //        source[0].PlayOneShot(clip[0]);
        //        audio0C = true;
        //    }

        //    if (timer > 3 && !audio1C)
        //    {
        //        source[1].PlayOneShot(clip[1]);
        //        audio1C = true;
        //    }

        //    if (timer > 6 && !audio2C)
        //    {
        //            //play pinting falling sound and animation
        //            paintingRB.useGravity = true;
        //            source[2].PlayOneShot(clip[2]);
        //            audio2C = true;
        //    }
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && !_doorIsOpen)
        {
            startSequence = true;
            StartCoroutine(_StartDoorLockedSequence());
        }

        if(other.tag == "Key" && !_doorIsOpen)
        {
            //fade to new scene
            StartCoroutine(_OpenTheDoor());
        }
    }

    private bool _doorIsOpen = false;
    private IEnumerator _OpenTheDoor()
    {
        _doorIsOpen = true;
        _StopPlayingTheSequence(); // Stop playing the sequence sounds if they are playing
        source[3].clip = clip[3];
        source[3].Play();
        float delay = clip[3].length - SceneEndedListener.FadeTime;
        if (delay < 1) delay = 1;
        yield return new WaitForSeconds(delay);
        EventsController.RegisterEvent(_sceneEndedEventName); // Will cause the scene ended listener to move to the next scene
    }


    private IEnumerator _StartDoorLockedSequence()
    {   
        for(int i = 0; i < 3; i++)
        {
            if (!_doorIsOpen)
            {
                source[i].clip = clip[i];
                source[i].Play();
                if (i == 2) paintingRB.useGravity = true;
                yield return new WaitForSeconds(clip[i].length);
            }
            else break;
        }
    }


    private void _StopPlayingTheSequence()
    {
        for(int i = 0; i < 3; i++)
        {
            source[i].Stop();
        }
    }
}
