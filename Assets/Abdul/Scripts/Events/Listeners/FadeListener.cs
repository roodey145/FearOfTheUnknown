using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeListener : Listener
{
    [SerializeField] protected float _fadeOutSpeedInSeconds = 3.0f;
    [SerializeField] Image _image;
    [SerializeField] bool _fadeIn = true;


    private float _fadeTimer = 0;
    private bool _fade = false;
    protected bool _fadeEnded = false;
    // Start is called before the first frame update
    void Start()
    {

        if (!_fadeIn) _fadeTimer = _fadeOutSpeedInSeconds;
        if (_image == null)
            _image = GetComponentInChildren<Image>();
        //EventsController.RegisterEvent(_eventName);
    }

    private float _alpha = 0;
    // Update is called once per frame
    void Update()
    {
        if (_fade && !_fadeEnded)
        {
            _alpha = _fadeTimer / _fadeOutSpeedInSeconds;
            _image.color = new Color(0, 0, 0, _alpha);

            _fadeTimer += (_fadeIn ? Time.deltaTime : -Time.deltaTime);

            if (_alpha > 1 || _alpha < 0)
            {
                _fadeEnded = true;
                _FadeEnd();
            }
        }
    }

    protected override void _Action()
    {
        _fade = true;
    }

    protected virtual void _FadeEnd()
    {
        _fadeEnded = true;
    }
}
