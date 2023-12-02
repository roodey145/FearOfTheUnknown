using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class PlayAudioListener : Listener
{
    protected AudioSource _audioSource;
    [SerializeField] protected AudioClip _clip;
    [SerializeField] protected float _delayInSeconds = 0f;
    [SerializeField] protected bool _repeat = false;

    [SerializeField] protected string _postTriggerEventName = "AudioEnded";

    // Start is called before the first frame update
    protected void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.Stop();
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
