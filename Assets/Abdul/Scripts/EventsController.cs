using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsController
{
    public static string currentEvent = "";
    public static List<EventListener> listeners = new List<EventListener>();


    public static void RegisterListener(EventListener lsitener)
    {
        listeners.Add(lsitener);
    }

    public static void RegisterEvent(string eventName)
    {
        currentEvent = eventName;
        for(int i = 0; i < listeners.Count; i++)
        {
            listeners[i].CheckCondition(eventName);
        }
    }
}
