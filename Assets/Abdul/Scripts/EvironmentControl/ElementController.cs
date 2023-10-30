using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementController : MonoBehaviour
{
    [SerializeField] private ObjectControlableProperitiesEnum controlableProperity;
    [SerializeField] private string _listenFor = "Stage_2";

    private void Awake()
    {
        // Create event listener
        EventListener eventListener = new EventListener(_CheckCondition, _ModifyObject);
        // Register the event
        EventsController.RegisterListener(eventListener);
        _RegisterProperities();
    }

    private void Start()
    {
        //StartCoroutine(RegisterEvent());
    }

    private IEnumerator RegisterEvent()
    { // Should be DeletedLater
        yield return new WaitForSeconds(3f);
        EventsController.RegisterEvent(_listenFor);
    }

    private void _RegisterProperities()
    {
        switch (controlableProperity)
        {
            case ObjectControlableProperitiesEnum.Hide:
                gameObject.SetActive(true);
                break;
            case ObjectControlableProperitiesEnum.Show:
                gameObject.SetActive(false);
                break;
        }
    }

    private void _ModifyObject()
    {
        switch (controlableProperity)
        {
            case ObjectControlableProperitiesEnum.Hide:
                gameObject.SetActive(false); 
                break;
            case ObjectControlableProperitiesEnum.Show:
                gameObject.SetActive(true); 
                break;
        }
    }

    private bool _CheckCondition(string value)
    {
        return value == _listenFor;
    }
}
