using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class AnimateListener : MonoBehaviour
{
    [SerializeField] private string _eventName;
    [SerializeField] private AnimationClipInfo[] _clipInfo;
    [SerializeField] private bool _repeat = false;
    [SerializeField] private float _repeatDelayInSeconds;
    [SerializeField] private Condition _repeatStopCondition; // Needed when _repeat is true

    private int _clipsPointer = 0;

    private Animator _animator;
    private AudioSource _audioSource;
    private bool _isPlaying = false;


    private void Awake()
    {
        EventListener eventListener = new EventListener( (eventName) => eventName == _eventName, _Execute );
        EventsController.RegisterListener( eventListener );
    }

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_isPlaying && _repeat && _repeatStopCondition != null)
        {
            if(_repeatStopCondition.Check())
            {
                _animator.enabled = false;
                _audioSource.enabled = false;

                // Stops the condition to reduce the computation overhead
                _repeatStopCondition.enabled = false;
                Component.Destroy(_repeatStopCondition);
                _isPlaying = false;
                EventsController.RegisterEvent("Animate");
            }
        }
    }


    private void _Execute(/*string eventName*/)
    {
        print("Executing");
        _animator.enabled = true;
        _audioSource.enabled = true;
        _isPlaying = true;
        _Play();
    }

    private void _Play()
    {
        if (_clipInfo == null || !_animator.enabled || !_audioSource.enabled) return;

        if (_repeat && _clipsPointer >= _clipInfo.Length) _clipsPointer = 0;

        _clipInfo[_clipsPointer++].Play(_animator, _audioSource, _Play);
    }


    #region 
    [CustomEditor(typeof(AnimateListener))]
    [CanEditMultipleObjects]
    public class MyScriptEditor : Editor
    {
        SerializedProperty _eventName;
        SerializedProperty _clipInfo;
        SerializedProperty _repeat;
        SerializedProperty _repeatDelayInSeconds;
        SerializedProperty _repeatStopCondition;

        void OnEnable()
        {
            _eventName = serializedObject.FindProperty("_eventName");
            _clipInfo = serializedObject.FindProperty("_clipInfo");
            _repeat = serializedObject.FindProperty("_repeat");
            _repeatDelayInSeconds = serializedObject.FindProperty("_repeatDelayInSeconds");
            _repeatStopCondition = serializedObject.FindProperty("_repeatStopCondition");
        }

        public override void OnInspectorGUI()
        {
            var myScript = (AnimateListener)target;
            serializedObject.Update();
            EditorGUILayout.PropertyField(_eventName);
            EditorGUILayout.PropertyField(_clipInfo);
            EditorGUILayout.PropertyField(_repeat);
            EditorGUILayout.PropertyField(_repeatDelayInSeconds);
            if (myScript._repeat)
                EditorGUILayout.PropertyField(_repeatStopCondition);
            serializedObject.ApplyModifiedProperties();
        }
    }
    #endregion
}



