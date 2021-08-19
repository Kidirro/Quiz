using System.Collections.Generic;
using UnityEngine;
using System;

public static class GlobalEvent
{
    static private Dictionary<TypesEvent, List<Action>> _callbacks = new Dictionary<TypesEvent, List<Action>>();

    public static void Subscribe(TypesEvent eventType, Action action)
    {
        if (!_callbacks.ContainsKey(eventType)) _callbacks[eventType] = new List<Action>();
        _callbacks[eventType].Add(action);
    }

    public static void Unsubscribe(TypesEvent eventType, Action action)
    {
        if (_callbacks.ContainsKey(eventType)) _callbacks[eventType].Remove(action);
    }

    public static void EventTrigger(TypesEvent eventType)
    {
        if (_callbacks.ContainsKey(eventType))
        {
            if (_callbacks[eventType].Count > 0)
            {
                foreach (Action a in _callbacks[eventType])
                {
                    try
                    {
                        a.Invoke();
                    }
                    catch (Exception e)
                    {
                        Debug.LogError(e);
                    }
                }
            }
        }
    }


}


public enum TypesEvent
{
   NewLevel,
   GameOver,
   GameStart,
   NewValue
}
