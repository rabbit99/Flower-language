using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Task : IDestroyable, IUpdatable, IEventListener
{

    public TaskManager TaskManager;
    public StateManager StateManager { get { return TaskManager.StateManager; } }


    protected class EventListenerData
    {
        public EventListener.EventCallback Callback;
        public bool CallWhenInactive;
    }


    protected Dictionary<int, EventListenerData> m_eventListeners;


    public Task()
    {
        m_eventListeners = new Dictionary<int, EventListenerData>();
    }



    //		public void ListenForEvent(int eventId, EventListener.EventCallback callback, bool callWhenInactive = false, int priority = 0)
    public void ListenForEvent(EGameEvents eventEnum, EventListener.EventCallback callback, bool callWhenInactive = false, int priority = 0)
    {
        EventListenerData eld = new EventListenerData();
        eld.Callback = callback;
        eld.CallWhenInactive = callWhenInactive;

        m_eventListeners[(int)eventEnum] = eld;
        Services.Get<EventManager>().RegisterListener((int)eventEnum, this, priority);
    }

    public void StopListeningForEvent(EGameEvents eventEnum)
    {
        Services.Get<EventManager>().UnregisterListener((int)eventEnum, this);
        m_eventListeners.Remove((int)eventEnum);
    }

    public bool IsListeningForEvent(EGameEvents eventEnum)
    {
        return m_eventListeners.ContainsKey((int)eventEnum);
    }


    public bool Active
    {
        get
        {
            return m_active;
        }

        set
        {
            m_active = value;

            if (m_active)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }


    public bool Visible
    {
        get
        {
            return m_visible;
        }

        set
        {
            m_visible = value;
            Show(m_visible);
        }
    }


    public bool AlwaysUpdates = false;


    #region IEventListener implementation
    public EventResult OnEvent(int eventId, object data)
    {
        if (m_eventListeners.ContainsKey(eventId))
        {
            EventListenerData eld = m_eventListeners[eventId];

            if (!Active && !eld.CallWhenInactive)
            {
                return null;
            }

            if (eld.Callback != null)
            {
                return eld.Callback(data);
            }
        }


        return null;
    }
    #endregion


    private bool m_active;
    private bool m_visible;



    public abstract void Pause();
    public abstract void Resume();
    public abstract void Show(bool show);
    public abstract void Update(float deltaTime);

    public virtual void Destroy()
    {
        foreach (int eventId in m_eventListeners.Keys)
        {
            Services.Get<EventManager>().UnregisterListener(eventId, this);
        }
        m_eventListeners.Clear();
    }
}
