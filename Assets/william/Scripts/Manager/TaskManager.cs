using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

public class TaskManager : IUpdatable, IDestroyable
{
    public delegate void OnStateChanged(long newState);

    public StateManager StateManager { get; private set; }
    public OnStateChanged StateChangedCallback { get; set; }
    public long CurrentState { get; private set; }


    public class TaskData
    {
        public Task Task;
        public long ActiveStates;
    }

    private int m_taskCount;
    public List<TaskData> Tasks { get { return m_tasks; } }
    private List<TaskData> m_tasks;
    private int m_lastStateId = 0;
    private bool hasSamplersClear = false;

#if UNITY_EDITOR
        public List<CustomSampler> TaskSamplers { get { return m_taskSamplers; } }
        private List<CustomSampler> m_taskSamplers;
#endif

    public TaskManager(StateManager stateManager)
    {
        m_taskCount = 0;
        m_tasks = new List<TaskData>();

#if UNITY_EDITOR
            m_taskSamplers = new List<CustomSampler>();
#endif

        StateManager = stateManager;
    }


    public long CreateState()
    {
        long result = 1L << m_lastStateId;
        Debug.Log("CreateState result = " + result);
        m_lastStateId++;

        return result;
    }


    public long AllStates()
    {
        long result = 0;

        for (int i = 0; i <= m_lastStateId; i++)
        {
            result |= 1L << i;
        }

        return result;
    }


    public static bool ContainsState(long stateFlags, long stateId)
    {
        return (stateFlags & stateId) == stateId;
    }


    public void ChangeState(long stateId)
    {
        CurrentState = stateId;

        for (int i = 0; i < m_taskCount; i++)
        {
            m_tasks[i].Task.Active = ContainsState(m_tasks[i].ActiveStates, stateId);
            if (m_tasks[i].Task.Active)
            {
                Debug.Log("m_tasks[i].Task.Active on, id = " + m_tasks[i].ActiveStates);
            }
        }

        if (StateChangedCallback != null)
        {
            StateChangedCallback(stateId);
        }
    }


    public void AddTask(Task task, long activeStates)
    {
        TaskData td = new TaskData();
        td.Task = task;
        task.TaskManager = this;
        td.ActiveStates = activeStates;

        m_tasks.Add(td);
#if UNITY_EDITOR
            m_taskSamplers.Add(CustomSampler.Create(task.GetType().Name));
#endif
        m_taskCount = m_tasks.Count;
    }


    public T GetTask<T>() where T : Task
    {
        for (int i = 0; i < m_taskCount; i++)
        {
            T result = m_tasks[i].Task as T;

            if (result != null)
            {
                return result;
            }
        }

        return null;
    }


    public void Update(float deltaTime)
    {
        for (int i = 0; i < m_taskCount; i++)
        {
#if UNITY_EDITOR
                m_taskSamplers[i].Begin();
#endif

            if (m_tasks[i].Task.Active || m_tasks[i].Task.AlwaysUpdates)
            {
                m_tasks[i].Task.Update(deltaTime);
            }

#if UNITY_EDITOR
            if(!hasSamplersClear)
            m_taskSamplers[i].End();
#endif
        }
    }


    public void Destroy()
    {
        for (int i = 0; i < m_taskCount; i++)
        {
            m_tasks[i].Task.TaskManager = null;
            m_tasks[i].Task.Destroy();
        }

        m_taskCount = 0;
        m_tasks.Clear();

#if UNITY_EDITOR
        hasSamplersClear = true;
        m_taskSamplers.Clear();
#endif

        m_lastStateId = 0;
    }
}
