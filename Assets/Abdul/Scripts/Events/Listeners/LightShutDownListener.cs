using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class LightShutDownListener : MonoBehaviour
{
    private Light _light;
    [SerializeField] private string _eventName = "ShutLightDown";
    [SerializeField] private bool _shutDown = false;
    [SerializeField] private bool _randomize = true;
    [Range(0f, 1f)]
    [SerializeField] private float _randomizePercentage = 0.1f;
    [SerializeField] private float _durationInSeconds = 3f; // Shuts comlpetely down after 3 second
    private float _durationCountDown;
    [SerializeField] private float _blinkingSpeedPerSecond = 5; // Blinks 5 time per second
    private float _blinkingTimer = 0;
    // Start is called before the first frame update
    void Start()
    {
        _light = GetComponent<Light>();

        if(_randomize)
        {
            _durationInSeconds += Random.Range(0, _durationInSeconds * _randomizePercentage);
            _blinkingSpeedPerSecond += Random.Range(0, _blinkingSpeedPerSecond * _randomizePercentage);
        }

        _durationCountDown = _durationInSeconds;

        EventListener eventListener = new EventListener((string eventName) => eventName == _eventName, ShutTheLightDown);
        EventsController.RegisterListener(eventListener);
        //EventsController.RegisterEvent(_eventName); // TODO: Delete Later
    }

    // Update is called once per frame
    void Update()
    {
        if(_shutDown)
        {
            float maxLightIntensity = (_durationCountDown / _durationInSeconds);

            if(_blinkingTimer >= 1f/_blinkingSpeedPerSecond)
            {
                float color = Random.Range(0, maxLightIntensity);
                _light.color = new Color(color, color, color);
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
        _shutDown = true;
    }
}
