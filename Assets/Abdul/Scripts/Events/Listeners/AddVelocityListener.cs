using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class AddVelocityListener : Listener
{
    [SerializeField] private Vector3 _forceToAddToVelocity = Vector3.zero;

    [SerializeField] private Rigidbody _rigidbody;

    protected override void _Action()
    {
         _rigidbody.velocity += _forceToAddToVelocity;
    }

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
