using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(AudioSource))]
public class GuideCharacterController : MonoBehaviour
{
    public static GuideCharacterController instance { get; private set; }

    [Header("Move Setting")]
    [SerializeField] private Transform[] _destinations;
    [SerializeField] private Vector3 _destination;
    [SerializeField] private string _moveEvent = "Move";
    [SerializeField] private string _moveAnimationTriggerName = "WalkTrigger";
    [SerializeField] private int _moveStageId = 0;

    [Header("Cinematic Settings")]
    //[SerializeField] private string[] _animationsTriggerName;
    [SerializeField] private AnimationClipInfo[] _animationsInfo;
    [SerializeField] private string _animateEvent = "Animate";
    [SerializeField] private int _animateStageId = 0;


    [SerializeField] private Animator _animator;
    [SerializeField] private AudioSource _audioSource;
    private NavMeshAgent _agent;

    private void Awake()
    {
        instance = this;

        EventListener moveEvent = new EventListener(
            (string eventName) => eventName == _moveEvent, 
            _MoveToNextDestination);

        EventListener animateEvent = new EventListener(
            (string eventName) => eventName == _animateEvent,
            _TriggerNextAnimation);

        // Register the listeners
        EventsController.RegisterListener(moveEvent);
        EventsController.RegisterListener(animateEvent);
    }

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        _audioSource = GetComponent<AudioSource>();

        // Trigger the first move event
        StartCoroutine(_TriggerEvent(_moveEvent, 0f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private IEnumerator _TriggerEvent(string eventName, float delayInSeconds)
    {
        yield return new WaitForSeconds(delayInSeconds);

        EventsController.RegisterEvent(eventName);
    }


    private void _MoveToNextDestination()
    {
        if (_destinations.Length <= _moveStageId) return;

        _destination = _destinations[_moveStageId].position;

        _agent.SetDestination(_destination);

        // Animate the character
        _animator.SetTrigger(_moveAnimationTriggerName);

        _moveStageId++;
    }

    private void _TriggerNextAnimation()
    {
        if (_animationsInfo.Length <= _animateStageId) return;

        _animationsInfo[_animateStageId].Play(_animator, _audioSource, _MoveToNextDestination);

        //_animator.SetTrigger(_animationsInfo[_animateStageId].animationTriggerName);

        //// Get the duration of the triggered animation
        //AnimationClip[] animations = _animator.runtimeAnimatorController.animationClips;
        //foreach (AnimationClip anim in animations)
        //{
        //    if (anim.name.ToLower() == _animationsInfo[_animateStageId].animationTriggerName.ToLower().Replace("trigger", ""))
        //    {
        //        StartCoroutine(_TriggerEvent(_moveEvent, anim.length)); // Trigger the move to next destination
        //    }
        //    else print("Animation Name: " + anim.name);
        //}

        _animateStageId++;
    }
}
