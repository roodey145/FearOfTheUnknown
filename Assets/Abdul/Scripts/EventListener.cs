using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventListener
{
    private Func<string, bool> _ListenerCheck;
    private Action _Action;
    public EventListener(Func<string, bool> condition, Action execute)
    {
        _ListenerCheck = condition;
        _Action = execute;
    }

    public void CheckCondition(string value)
    {
        if(_ListenerCheck(value))
        {
            _Action();
        }
    }
}
