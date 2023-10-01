using System.Collections.Generic;
using Godot;
using Thrones.Scripts.States;

namespace Thrones.Scripts;

// implement transition methods? from to?
public partial class StateManager : Node
{
    /// Events
    [Signal] public delegate void StateChangeEventHandler(bool replaceState, IState nextState);

    /// Data Fields
    private Stack<IState> _stateStack;
    public IState CurrentState => PeekState();
    private List<Transition> _transitions;

    public StateManager()
    {
        Name = "StateManager";
    }

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
        CurrentState?.Execute();
    }

    private IState PeekState()
    {
        return _stateStack.Count > 0 ? _stateStack.Peek() : null;
    }

    private void ResolveTransitions(IReadOnlyList<Transition> transition)
    {
        for (var i = 0; i < transition.Count; i++)
        {
            if (transition[i] == null) continue;
            switch (transition[i].ReplaceState)
            {
                case true:
                    ReplaceState(transition[i].Next);
                    _transitions.Remove(transition[i]);
                    break;

                case false:
                    AddState(transition[i].Next);
                    _transitions.Remove(transition[i]);
                    break;
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
        _stateStack?.Pop();

        _stateStack?.Push(nextState);
        CurrentState?.Enter();
    }

    private void OnTransition(bool replaceState, IState state = null)
    {
        var t = new Transition(state, replaceState);
        _transitions.Add(t);
    }

    private sealed class Transition
    {
        public readonly IState Next;
        public readonly bool ReplaceState;

        public Transition(IState next, bool replaceState)
        {
            Next = next;
            ReplaceState = replaceState;
        }
    }
}