using System.Collections.Generic;
using UnityEngine;

public interface IStateMachine
{
    void RegisterState<T>(IState state) where T : IState;
    void SetState<T>() where T : IState;
}

public class StateMachine : IStateMachine
{
    private IState _currentState;

    private Dictionary<System.Type, IState> _states = new();

    public void RegisterState<T>(IState state) where T : IState
    {
        _states[typeof(T)] = state;
    }

    public void SetState<T>() where T : IState
    {
        if (_states.TryGetValue(typeof(T), out var newState))
        {
            _currentState?.Exit();
            _currentState = newState;
            _currentState.Enter();
        }
        else
        {
            Debug.LogError($"State of type {typeof(T)} not registered.");
        }
    }
}
