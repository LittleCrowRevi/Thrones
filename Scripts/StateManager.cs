using Godot;
using System.Collections.Generic;
using Thrones.Scripts.States;

namespace Thrones.Scripts
{
    // implement transition methods? from to?
    public partial class StateManager : Node
    {
        /// Data Fields
        private Stack<IState> stateStack;

        public IState CurrentState => PeekState();
        private List<Transition> Transitions;

        /// Events
        [Signal] public delegate void StateChangeEventHandler(bool replaceState, IState nextState);

        /// methods

        public StateManager()
        {
            this.Name = "StateManager";
        }

        public override void _Ready()
        {
            stateStack = new Stack<IState>();
            Transitions = new List<Transition>();
            StateChange += OnTransition;
        }

        public override void _Process(double delta)
        {
            Update();
        }

        private void Update()
        {
            if (Transitions.Count > 0)
            {
                ResolveTransitions(Transitions);
            }
            CurrentState?.Execute();
        }

        private IState PeekState()
        {
            return stateStack.Count > 0 ? stateStack.Peek() : null;
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
                        Transitions.Remove(transition[i]);
                        break;

                    case false:
                        AddState(transition[i].Next);
                        Transitions.Remove(transition[i]);
                        break;
                }
            }
        }

        public void InitialState(IState state)
        {
            stateStack.Clear();
            stateStack.Push(state);

            CurrentState?.Enter();
        }

        private void AddState(IState nextState)
        {
            if (nextState == CurrentState) return;

            CurrentState?.Exit();

            stateStack?.Push(nextState);
            CurrentState?.Enter();
        }

        private void RemoveState()
        {
            bool stackEmpty = stateStack is not { Count: > 1 };
            if (stackEmpty) return;

            CurrentState?.Exit();
            stateStack.Pop();
        }

        private void ReplaceState(IState nextState)
        {
            if (nextState == CurrentState) return;

            CurrentState?.Exit();
            stateStack?.Pop();

            stateStack?.Push(nextState);
            CurrentState?.Enter();
        }

        private void OnTransition(bool replaceState, IState state = null)
        {
            var t = new Transition(state, replaceState);
            Transitions.Add(t);
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
}