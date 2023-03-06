using System.Collections.Generic;
using UnityEngine;

//The scriptable object here acts as a mechanism for decoupling events and the event responses
[CreateAssetMenu]
public class ScoreEvent : ScriptableObject
{
    private List<IEventListener> listeners = new List<IEventListener>();

    //Any object that needs to notify listeners of a score updation event can raise the event
    //Listeners of the event will receive the event and respond
    public void Raise(int score)
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].OnEventRaised(score);
        }
    }

    public void RegisterEventListener(IEventListener listener)
    {
        listeners.Add(listener);
    }
    public void UnRegisterEventListener(IEventListener listener)
    {
        listeners.Remove(listener);
    }
}
