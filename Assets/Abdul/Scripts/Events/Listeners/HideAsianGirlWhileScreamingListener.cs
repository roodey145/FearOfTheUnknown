using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideAsianGirlWhileScreamingListener : PlayAudioListener
{
    [SerializeField] private SkinnedMeshRenderer _meshRenderer;

    private new void Start()
    {
        base.Start();
        _meshRenderer = GetComponent<SkinnedMeshRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        if(_audioSource.isPlaying && _meshRenderer.enabled)
        {
            _meshRenderer.enabled = false;
        }
    }
}
