using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource)), RequireComponent(typeof(Collider)), RequireComponent(typeof(Rigidbody))]
public class GirlStepSoundTrigger : MonoBehaviour
{
    [SerializeField] private string _triggerActivatorTag = "Geometry";
    [SerializeField] private AudioClip _stepSound;
    private bool _triggered = false;

    private Rigidbody _rigidbody;
    private AudioSource _audioSource;
    private Collider _collider;

    // Start is called before the first frame update
    void Start()
    {
        // Make sure that the rigid body is active to be able to activate trigger
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.useGravity = false;

        // Make sure the sound is 3D
        _audioSource = GetComponent<AudioSource>();
        _audioSource.spatialBlend = 0.9f;

        // Make sure the is trigger active to detect any trigger
        _collider = GetComponent<Collider>();
        _collider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!_triggered && other.CompareTag(_triggerActivatorTag))
        {
            _triggered = true;
            // Play the step sound
            _audioSource.clip = _stepSound;
            _audioSource.time = 0;
            _audioSource.Play();
            //_audioSource.PlayOneShot(_stepSound);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (_triggered && other.CompareTag(_triggerActivatorTag))
        {
            _triggered = false;
        }
    }
}
