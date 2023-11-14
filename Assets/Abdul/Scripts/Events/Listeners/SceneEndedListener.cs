using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneEndedListener : Listener
{
    [SerializeField] float _fadeOutSpeedInSeconds = 3.0f;
    [SerializeField] Image _image;
    private float _fadeOutTimer = 0;
    private bool _fadeOut = false;
    // Start is called before the first frame update
    void Start()
    {
        _fadeOutTimer = _fadeOutSpeedInSeconds;
        if(_image == null)
            _image = GetComponentInChildren<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if( _fadeOut )
        {

            _image.color = new Color(0, 0, 0, _fadeOutTimer / _fadeOutSpeedInSeconds);
            _fadeOutTimer -= Time.deltaTime;

            if(_fadeOutTimer <= 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // Open the next scene
            }
        }
    }

    protected override void _Action()
    {
        _fadeOut = true;
    }
}
