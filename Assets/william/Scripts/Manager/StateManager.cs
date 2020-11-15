using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : IUpdatable
{
    public bool IsPaused { get; set; }
    public GameState CurrentState { get; private set; }


    public StateManager()
    {
        IsPaused = false;
        CurrentState = null;
    }


    public void ChangeState(GameState newState)
    {

        if (CurrentState != null)
        {
            CurrentState.Destroy();
        }
        CurrentState = newState;
        if (!CurrentState.HasBeenInited)
        {
            CurrentState.Init();
            CurrentState.HasBeenInited = true;
        }
        CurrentState.Enter();
    }


    public void Update(float deltaTime)
    {
        if (CurrentState != null && !IsPaused)
        {
            CurrentState.Update(deltaTime);
        }
    }
}
