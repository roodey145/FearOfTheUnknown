using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class PlayAudioListener : Listener
{
    private AudioSource _audioSource;
    [SerializeField] private AudioClip _clip;

    [SerializeField] private string _postTriggerEventName = "AudioEnded";

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    protected override void _Action()
    {
        _audioSource.clip = _clip;
        _audioSource.Play();
        StartCoroutine(_TriggerPostEvent());
    }


    private IEnumerator _TriggerPostEvent()
    {
        yield return new WaitForSeconds(_audioSource.clip.length);
        EventsController.RegisterEvent(_postTriggerEventName);
    }
}
