using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class paperController : MonoBehaviour
{
    public AudioSource source;
    public AudioClip clip;
    public GameObject canvas;
    public Light spotLight;

    public InputActionReference[] actions;

    private bool newpaperOpen;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < actions.Length; i++)
        {
            actions[i].action.performed += _HideNewsPaper;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //delete later. just for testing should be used for the controller and not keyboard
        if (newpaperOpen == true && Input.GetKeyDown(KeyCode.E))// +controller input button  -Input.GetKeyDown(KeyCode.E)
        {
            canvas.SetActive(false);
            newpaperOpen = false;
            heartBeat.instance.heartBeating = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")// && controller input button
        {
            source.PlayOneShot(clip);
            canvas.SetActive(true);
            // change camera angle to look towards the room
            newpaperOpen = true;
            spotLight.enabled = true;
            
        }

    }

    private void _HideNewsPaper(InputAction.CallbackContext callbackContext)
    {
        if (newpaperOpen == true)// +controller input button  -Input.GetKeyDown(KeyCode.E)
        {
            canvas.SetActive(false);
            newpaperOpen = false;
            heartBeat.instance.heartBeating = true;
        }
    }
}
