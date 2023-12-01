using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginAct : MonoBehaviour
{
    public AudioSource source;
    public AudioClip clip;
    public float _delayInSeconds = 3;
    public float _endAtSecond = -1;


    private void Start()
    {
        StartCoroutine(_StartScene());
    }


    private IEnumerator _StartScene()
    {
        yield return new WaitForSeconds(_delayInSeconds);
        source.clip = clip;
        source.Play();
        if(_endAtSecond > 0)
        {
            yield return new WaitForSeconds(_endAtSecond);
            source.Stop();
        }
    }
}
