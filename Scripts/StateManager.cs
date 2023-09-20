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

        public void Update()
        {
            if (Transitions.Count > 0)
            {
                ResolveTransitions(Transitions);
            }
            CurrentState?.Execute();
        }

        private IState PeekState()
        {
            if (stateStack.Count > 0)
            {
                return stateStack.Peek();
            }
            return null;
        }

        private void ResolveTransitions(List<Transition> transition)
        {
            for (int i = 0; i < transition.Count; i++)
            {
                var transitionItem = transition[i];
                if (transitionItem == null)
                {
                    continue;
                }
                switch (transitionItem.replaceState)
                {
                    case true:
                        ReplaceState(transitionItem.Next);
                        Transitions.Remove(transitionItem);
                        break;

                    case false:
                        AddState(transitionItem.Next);
                        Transitions.Remove(transitionItem);
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

        public void AddState(IState nextState)
        {
            if (nextState == CurrentState) return;

            CurrentState?.Exit();

            stateStack?.Push(nextState);
            CurrentState?.Enter();
        }

        public void RemoveState()
        {
            bool stackEmpty = stateStack == null || stateStack.Count <= 1;
            if (stackEmpty) return;

            CurrentState?.Exit();
            stateStack.Pop();
        }

        public void ReplaceState(IState nextState)
        {
            if (nextState == CurrentState) return;

            CurrentState?.Exit();
            stateStack?.Pop();

            stateStack?.Push(nextState);
            CurrentState?.Enter();
        }

        public void OnTransition(bool replaceState, IState state = null)
        {
            var t = new Transition(state, replaceState);
            Transitions.Add(t);
        }

        private sealed class Transition
        {
            public IState Next;
            public bool replaceState;

            public Transition(IState next, bool replaceState)
            {
                Next = next;
                this.replaceState = replaceState;
            }
        }
    }
}