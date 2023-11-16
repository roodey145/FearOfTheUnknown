using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class PlayAudioListener : Listener
{
    private AudioSource _audioSource;
    [SerializeField] private AudioClip _clip;
    [SerializeField] private float _delayInSeconds = 0f;
    [SerializeField] private bool _repeat = false;

    [SerializeField] private string _postTriggerEventName = "AudioEnded";

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_repeat && !_audioSource.isPlaying)
        {
            _audioSource.Play();
        }
    }


    protected override void _Action()
    {
        StartCoroutine(_PlayDelayedAudio());
    }


    private IEnumerator _PlayDelayedAudio()
    {
        yield return new WaitForSeconds(_delayInSeconds);

        _audioSource.clip = _clip;
        _audioSource.Play();

        yield return new WaitForSeconds(_audioSource.clip.length);
        EventsController.RegisterEvent(_postTriggerEventName);
    }
}
