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
    [SerializeField] private DestinationInfo[] _destinationsInfo;
    [SerializeField] private int _destinationPointer = 0;
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
    private float _agentInitialSpeed = 0;

    private bool _matchRotation = false;
    private Quaternion _rotation;

    private bool _moving = false;

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

        _agentInitialSpeed = _agent.speed;

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
        //print($"Destination Pointer {_destinationPointer} , Destination Length: {_destinationsInfo.Length}");

        if (_moving) _destinationPointer++;

        if (_destinationsInfo.Length <= _destinationPointer) return;

        

        _moving = true;

        // Make sure that this animation has a destination
        if (_destinationsInfo[_destinationPointer].destination != null)
        {
            _destination = _destinationsInfo[_destinationPointer].destination.position;
        }

        // Increase the agent speed if the current animation requires that
        _agent.speed = _agentInitialSpeed + _destinationsInfo[_destinationPointer].speedIncrease;

        _agent.SetDestination(_destination);

        // Animate the character
        _animator.SetTrigger(_destinationsInfo[_destinationPointer].movingAnimationTriggerName);
    }

    private void _TriggerNextAnimation()
    {
        if (_destinationsInfo.Length <= _destinationPointer) return;

        // Check if we need to match the destination rotation
        if(_destinationsInfo[_destinationPointer].matchDestinationRotation)
        {
            transform.rotation = _destinationsInfo[_destinationPointer].destination.rotation;
        }

        _destinationsInfo[_destinationPointer].clip.Play(_animator, _audioSource, _MoveToNextDestination);

        _moving = false;
        _destinationPointer++;
    }
}
