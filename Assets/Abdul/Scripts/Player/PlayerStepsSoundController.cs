using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(AudioSource))]
public class PlayerStepsSoundController : MonoBehaviour
{
    [SerializeField] private AudioClip _stepSound;
    [SerializeField] private InputActionReference _moveAction;
    [SerializeField] private ActionBasedContinuousMoveProvider _moveProvider;
    [SerializeField] private float _volume = 0.35f;
    [SerializeField] private float _spatialBlend = 1.0f;
    private bool _playing = false;

 
    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        // Make sure the sound is 3D
        _audioSource = GetComponent<AudioSource>();
        _audioSource.spatialBlend = _spatialBlend;
        _audioSource.volume = _volume;

        // Register a the play move sound method to the move event
        if(_moveAction != null)
        {
            _moveAction.action.performed += _PlayMoveSound;
        }

        if(_moveProvider == null)
        {
            _moveProvider = GameObject.FindGameObjectWithTag("Player").GetComponent<ActionBasedContinuousMoveProvider>();
        }
    }

    private void _PlayMoveSound(InputAction.CallbackContext context)
    {
        if(_audioSource != null && !_audioSource.isPlaying && !context.canceled && _moveProvider.moveSpeed > 0)
        {
            _audioSource.clip = _stepSound;
            _audioSource.time = 0;
            _audioSource.Play();
        }
    }

    private void OnDestroy()
    {
        _moveAction.action.performed -= _PlayMoveSound;
    }
}
