using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class MonsterChase : Listener
{
    private bool _startChasing = false;
    [SerializeField] private float _speed = 1f;
    [SerializeField] private Axis _controlledAxis = Axis.X;
    [SerializeField] private string _targetTag = "Player";
    [SerializeField] private string _eventToTriggerWhenPlayerCaught = "PlayerCaught";

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<Collider>().isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(_startChasing)
        {
            switch(_controlledAxis)
            {
                case Axis.X:
                    transform.position += new Vector3(_speed * Time.deltaTime, 0, 0);
                    break;
                case Axis.Y:
                    transform.position += new Vector3(0, _speed * Time.deltaTime, 0);
                    break;
                case Axis.Z:
                    transform.position += new Vector3(0, 0, _speed * Time.deltaTime);
                    break;
            }
        }
    }

    protected override void _Action()
    {
        _startChasing = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(_targetTag))
        {
            EventsController.RegisterEvent(_eventToTriggerWhenPlayerCaught);
        }
    }
}
