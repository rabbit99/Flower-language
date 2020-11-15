using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventListener : EventListener
{

    public virtual void ListenForEvent(EGameEvents eventEnum, EventCallback callback, int priority = 0)
    {
        base.ListenForEvent((int)eventEnum, callback, priority);
    }
}

public abstract class GameState : IDestroyable, IUpdatable
{
    public StateManager StateManager { get; private set; }
    public TaskManager TaskManager { get; private set; }
    public MainSystem MainSystem { get; private set; }
    public bool HasBeenInited { get; set; }

    protected GameEventListener m_eventListener;

    public GameState(StateManager statemanager, MainSystem mainSystem)
    {
        StateManager = statemanager;
        MainSystem = mainSystem;
        TaskManager = new TaskManager(statemanager);
        HasBeenInited = false;
        m_eventListener = new GameEventListener();
    }

    public bool IsListeningForEvent(EGameEvents eventEnum)
    {
        return m_eventListener.isListeningForEvent((int)eventEnum);
    }

    public void ListenForEvent(EGameEvents eventEnum, EventListener.EventCallback callback, int priority = 0)
    {
        m_eventListener.ListenForEvent(eventEnum, callback, priority);
    }

    public void StopListenForEvent(EGameEvents eventEnum)
    {
        m_eventListener.StopListeningForEvent((int)eventEnum);
    }


    public abstract void Init();

    public abstract void Enter();

    public virtual void Update(float deltaTime)
    {
        TaskManager.Update(deltaTime);
    }


    public virtual void Destroy()
    {
        if (m_eventListener != null)
        {
            m_eventListener.Destroy();
            m_eventListener = null;
        }

        TaskManager.Destroy();
    }
}
