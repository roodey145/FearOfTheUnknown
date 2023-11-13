using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class LightShutDownListener : MonoBehaviour
{
    private Light _light;
    private bool _eventTriggered = false;
    [SerializeField] private string _eventName = "ShutLightDown";
    [SerializeField] private bool _blink = false;
    [SerializeField, Range(0f, 1f)] private float _maxLightIntensity = 1;
    [SerializeField, Range(0f, 1f)] private float _minLightIntensity = 0.5f;
    [SerializeField] private bool _shutDown = false;
    [SerializeField] private bool _randomize = true;
    [SerializeField, Range(0f, 1f)] private float _randomizePercentage = 0.1f;
    [SerializeField] private float _durationInSeconds = 3f; // Shuts comlpetely down after 3 second
    private float _durationCountDown;
    [SerializeField] private float _blinkingSpeedPerSecond = 5; // Blinks 5 time per second
    private float _blinkingTimer = 0;

    [SerializeField] private string _emissionColorName = "_EmissionColor";
    private MeshRenderer _renderer;
    private Material _material;

    // Start is called before the first frame update
    void Start()
    {
        _light = GetComponent<Light>();
        _renderer = transform.parent.GetComponent<MeshRenderer>();
        _material = _renderer.material;

        if (_randomize)
        {
            _durationInSeconds += UnityEngine.Random.Range(0, _durationInSeconds * _randomizePercentage);
            _blinkingSpeedPerSecond += UnityEngine.Random.Range(0, _blinkingSpeedPerSecond * _randomizePercentage);
        }

        _durationCountDown = _durationInSeconds;

        EventListener eventListener = new EventListener((string eventName) => eventName == _eventName, ShutTheLightDown);
        EventsController.RegisterListener(eventListener);
        //EventsController.RegisterEvent(_eventName); // TODO: Delete Later
    }

    

    // Update is called once per frame
    void Update()
    {
        if( !_eventTriggered && _blink )
        {
            if (_blinkingTimer >= 1f / _blinkingSpeedPerSecond)
            {
                _ChangeLightColor();
                _blinkingTimer = 0;
            }
            _blinkingTimer += Time.deltaTime;
        }

        if(_shutDown)
        {
            _maxLightIntensity = (_durationCountDown / _durationInSeconds);

            if(_blinkingTimer >= 1f/_blinkingSpeedPerSecond)
            {
                _ChangeLightColor();
                _blinkingTimer = 0;
            }

            _durationCountDown -= Time.deltaTime;
            _blinkingTimer += Time.deltaTime;

            if(_durationCountDown <= 0)
            {
                _light.color = Color.black;
                _shutDown = false;
            }
        }
    }


    public void ShutTheLightDown()
    {
        _minLightIntensity = 0;
        _eventTriggered = true;
        _shutDown = true;
    }

    private void _ChangeLightColor()
    {
        float color = UnityEngine.Random.Range(_minLightIntensity, _maxLightIntensity);
        _light.color = new Color(color, color, color);
        _material.SetColor(_emissionColorName, _light.color);
        _renderer.material = _material;
    }
}
