using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginAct : MonoBehaviour
{
    public AudioSource source;
    public AudioClip clip;
    public float _delayInSeconds = 3;
    public float _startTime = 6;

    private void Start()
    {
        StartCoroutine(_StartScene());
    }
    private IEnumerator _StartScene()
    {
        yield return new WaitForSeconds(_delayInSeconds);
        source.clip = clip;
        source.time = _startTime;
        source.Play();
        //source.PlayOneShot(clip);
    }
}
