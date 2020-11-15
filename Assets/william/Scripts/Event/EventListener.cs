using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventListener : IEventListener, IDestroyable
{

    public delegate EventResult EventCallback(object eventData);
    protected Dictionary<int, EventCallback> m_eventListeners;


    public EventListener()
    {
        m_eventListeners = new Dictionary<int, EventCallback>();
    }


    #region IEventListener implementation
    public EventResult OnEvent(int eventId, object data)
    {
        if (m_eventListeners.ContainsKey(eventId))
        {
            EventCallback callback = m_eventListeners[eventId];

            if (callback != null)
            {
                return callback(data);
            }
        }

        return null;
    }
    #endregion


    public bool isListeningForEvent(int eventId)
    {
        return m_eventListeners.ContainsKey(eventId);
    }


    public virtual void ListenForEvent(int eventId, EventCallback callback, int priority = 0)
    {
        m_eventListeners[eventId] = callback;
        Services.Get<EventManager>().RegisterListener(eventId, this, priority);
    }


    public virtual void StopListeningForEvent(int eventId)
    {
        Services.Get<EventManager>().UnregisterListener(eventId, this);
        m_eventListeners.Remove(eventId);
    }


    public virtual void Destroy()
    {
        foreach (int eventId in m_eventListeners.Keys)
        {
            Services.Get<EventManager>().UnregisterListener(eventId, this);
        }
        m_eventListeners.Clear();
    }
}
