using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerEventsListener : MonoBehaviour
{
    [SerializeField] private AudioClip _clip;
    [SerializeField] private string _eventName = "DoorSlammed";
    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();

        EventListener eventListener = new EventListener((string eventName) => eventName == _eventName, PlayAudio);
        EventsController.RegisterListener(eventListener);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayAudio()
    {
        _audioSource.clip = _clip;
        _audioSource.Play();
    }
}
