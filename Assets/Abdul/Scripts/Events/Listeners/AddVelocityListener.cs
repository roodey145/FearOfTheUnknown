using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class AddVelocityListener : Listener
{
    [Header("Froce Settings")]
    [SerializeField] private Vector3 _forceToAddToVelocity = Vector3.zero;
    [SerializeField] private Space _forceSpace = Space.World;

    private Rigidbody _rigidbody;

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
