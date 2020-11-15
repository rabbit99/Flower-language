using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager
{

    public delegate void OnEventSendDelegate(int eventId, object data);

    public OnEventSendDelegate OnEventSentCallback { get; set; }

    private bool lockForButoon = false;

    private class ListenerContainer
    {
        public IEventListener Listener { get; private set; }
        public int Priority { get; private set; }

        public ListenerContainer(IEventListener listener, int priority)
        {
            Priority = priority;
            Listener = listener;
        }
    }


    private List<ListenerContainer>[] m_eventListeners;

    public EventManager()
    {
        OnEventSentCallback = null;

        int enumCount = Enum.GetNames(typeof(EGameEvents)).Length;
        m_eventListeners = new List<ListenerContainer>[enumCount];

        for (int i = 0; i < enumCount; i++)
        {
            m_eventListeners[i] = new List<ListenerContainer>();
        }
    }

    private int GetEventIdx(int eventId)
    {
        return eventId - (int)EGameEvents.Invalid - 1;
    }

    //higher priority numbers get sent the event first
    public void RegisterListener(int eventId, IEventListener listener, int priority = 0)
    {
        int eventIdx = GetEventIdx(eventId);

        List<ListenerContainer> listeners = m_eventListeners[eventIdx];


        foreach (ListenerContainer l in listeners)
        {
            if (l.Listener == listener)
            {
                Debug.LogException(new Exception("Listener is already registered for this object! eventId=" + (EGameEvents)eventId));
                return;
            }
        }

        listeners.Add(new ListenerContainer(listener, priority));
        Comparison<ListenerContainer> c = delegate (ListenerContainer lc1, ListenerContainer lc2) { return lc2.Priority.CompareTo(lc1.Priority); };
        listeners.Sort(c);
    }


    public void UnregisterListener(int eventId, IEventListener listener)
    {
        int eventIdx = GetEventIdx(eventId);

        if (eventIdx < 0 || eventIdx >= m_eventListeners.Length)
        {
            Debug.LogErrorFormat("eventIdx = {0} is out of range", eventIdx);
            return;
        }


        List<ListenerContainer> listeners = m_eventListeners[eventIdx];
        ListenerContainer lc;

        for (int i = 0, count = listeners.Count; i < count; i++)
        {
            lc = listeners[i];

            if (lc.Listener == listener)
            {
                m_eventListeners[eventIdx].RemoveAt(i);
                return;
            }
        }

    }

    public EventResult SendEvent(int eventId, object data = null, bool isFromButton = false)
    {
        if (lockForButoon && isFromButton)
        {
            return null;
        }
        if (OnEventSentCallback != null)
        {
            OnEventSentCallback(eventId, data);
        }

        int eventIdx = GetEventIdx(eventId);

        if (eventIdx < 0 || eventIdx >= m_eventListeners.Length)
        {
            Debug.LogErrorFormat("eventIdx = {0} is out of range", eventIdx);
            return null;
        }

        //List ToArray will trigger an "Array Copy" then take 32B GC alloc, but we must do this, because the m_eventListeners might be add or removed....
        ListenerContainer[] listeners = m_eventListeners[eventIdx].ToArray();
        ListenerContainer lc;
        //clone listeners before iteration to allow removing a listener in the event handler
        for (int i = 0, count = listeners.Length; i < count; i++)
        {
            lc = listeners[i];
            if (null == lc)
            {
                continue;
            }
            EventResult result = lc.Listener.OnEvent(eventId, data);

            if (result == null)
            {
                continue;
            }

            if (result.WasEaten)
            {
                return result;
            }
        }

        return null;
    }

    public void LockGateForButtonAction()
    {
        lockForButoon = true;
    }
    public void UnlockGateForButtonAction()
    {
        lockForButoon = false;
    }
}
