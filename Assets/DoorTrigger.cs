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
            
            Debug.Log("YOOO");
            //
            //fade to new scene
            StartCoroutine(_OpenTheDoor());
        }
    }

    private bool _doorIsOpen = false;
    private IEnumerator _OpenTheDoor()
    {
        _doorIsOpen = true;
        source[3].PlayOneShot(clip[3]);
        yield return new WaitForSeconds(clip[3].length);
        EventsController.RegisterEvent(_sceneEndedEventName); // Will cause the scene ended listener to move to the next scene
    }


    private IEnumerator _StartDoorLockedSequence()
    {   
        source[0].PlayOneShot(clip[0]);
        yield return new WaitForSeconds(clip[0].length); // Wait until clip One ends
        source[1].PlayOneShot(clip[1]);
        yield return new WaitForSeconds(clip[1].length); // Wait until clip Two ends
        paintingRB.useGravity = true;
        source[2].PlayOneShot(clip[2]);
    }
}
