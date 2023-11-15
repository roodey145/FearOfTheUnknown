using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ShutEnvironmentLightDownListener : Listener
{
    private AudioSource _audioSource;
    [SerializeField] private AudioClip _clip;
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
        RenderSettings.ambientLight = Color.black;
        _audioSource.clip = _clip;
        _audioSource.Play();
    }
}
