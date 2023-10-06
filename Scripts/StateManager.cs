using System;
using System.Collections.Generic;
using Godot;
using Thrones.Scripts.States;
using ThronesEra;

namespace Thrones.Scripts;

public enum TransitionType
{
    /// <summary>
    /// Add the new State upon the stack without removing the previous State.
    /// </summary>
    Add,
    /// <summary>
    /// Replace the previous State on the stack with the new State.
    /// </summary>
    Replace,
    /// <summary>
    /// Remove the current State from the stack.
    /// </summary>
    Remove
}

// implement transition methods? from to?
public partial class StateManager : Node
{
    public StateManager()
    {
        Name = "StateManager";
    }
    
    /// Events
    [Signal] public delegate void StateChangeEventHandler(int transitionType, IState nextState);

    /// Data Fields
    private Stack<IState> _stateStack;
    public IState CurrentState => PeekState();
    private List<Transition> _transitions;

    /// methods
    public override void _Ready()
    {
        _stateStack = new Stack<IState>();
        _transitions = new List<Transition>();
        StateChange += OnTransition;
    }

    public override void _Process(double delta)
    {
        Update();
    }

    private void Update()
    {
        if (_transitions.Count > 0) ResolveTransitions(_transitions);
        if (CurrentState is not null)
        {
            CurrentState.Execute();
        }
    }

    private IState PeekState()
    {
        return _stateStack.Count > 0 ? _stateStack.Peek() : null;
    }

    private void ResolveTransitions(IReadOnlyList<Transition> transitions)
    {
        if (transitions.Count == 0) return;
        for (var index = 0; index < transitions.Count; index++)
        {
            var transition = transitions[index];
            if (transition == null) continue;
            Logger.INFO("Transitioning to State: " + transition.Next.Name);
            switch (transition.TransitionType)
            {
                case TransitionType.Replace:
                    ReplaceState(transition.Next);
                    _transitions.Remove(transition);
                    break;
                case TransitionType.Add:
                    AddState(transition.Next);
                    _transitions.Remove(transition);
                    break;
                case TransitionType.Remove:
                    RemoveState();
                    _transitions.Remove(transition);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(transitions), "TransitionType missing");
            }
        }
    }

    public void InitialState(IState state)
    {
        _stateStack.Clear();
        _stateStack.Push(state);

        CurrentState?.Enter();
    }

    private void AddState(IState nextState)
    {
        if (nextState == CurrentState) return;

        CurrentState?.Exit();

        _stateStack?.Push(nextState);
        CurrentState?.Enter();
    }

    private void RemoveState()
    {
        var stackEmpty = _stateStack is not { Count: > 1 };
        if (stackEmpty) return;

        CurrentState?.Exit();
        _stateStack.Pop();
    }

    private void ReplaceState(IState nextState)
    {
        if (nextState == CurrentState) return;

        CurrentState?.Exit();
        if (_stateStack.Count > 0) _stateStack.Pop();
        _stateStack.Push(nextState);
        CurrentState?.Enter();
    }

    private void OnTransition(int transitionType, IState state = null)
    {
        Logger.INFO("TransitionType: " + (TransitionType)transitionType);
        var t = new Transition((TransitionType)transitionType, state);
        _transitions.Add(t);
    }

    private sealed class Transition
    {
        public readonly IState Next;
        public readonly TransitionType TransitionType;

        public Transition(TransitionType transitionType, IState next = null)
        {
            Next = next;
            TransitionType = transitionType;
        }
    }
}