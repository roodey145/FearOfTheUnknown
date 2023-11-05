using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(NavMeshAgent))]
public class GuideCharacterController : MonoBehaviour
{
    [Header("Move Setting")]
    [SerializeField] private Transform[] _destinations;
    [SerializeField] private Vector3 _destination;
    [SerializeField] private string _moveEvent = "Move";
    [SerializeField] private string _moveAnimationTriggerName = "WalkTrigger";
    [SerializeField] private int _moveStageId = 0;

    [Header("Cinematic Settings")]
    [SerializeField] private string[] _animationsTriggerName;
    [SerializeField] private string _animateEvent = "Animate";
    [SerializeField] private int _animateStageId = 0;


    [SerializeField] private Animator _animator;
    private NavMeshAgent _agent;

    private void Awake()
    {
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
        if (_animationsTriggerName.Length <= _animateStageId) return;

        _animator.SetTrigger(_animationsTriggerName[_animateStageId]);

        // Get the duration of the triggered animation
        AnimationClip[] animations = _animator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip anim in animations)
        {
            if (anim.name.ToLower() == _animationsTriggerName[_animateStageId].ToLower().Replace("trigger", ""))
            {
                StartCoroutine(_TriggerEvent(_moveEvent, anim.length)); // Trigger the move to next destination
            }
            else print("Animation Name: " + anim.name);
        }

        _animateStageId++;
    }
}
