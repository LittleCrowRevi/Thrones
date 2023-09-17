using Thrones.Scripts.States;
using System;
using System.Collections.Generic;
using Godot;

namespace Thrones.Scripts
{
    // implement transition methods? from to?
    public partial class StateManager : Node
    {

        // Data Fields
        private Stack<IState> stateStack;
        public IState CurrentState => stateStack?.Peek();
        private List<Transition> Transitions;

        // Events
        [Signal] public delegate void StateChangeEventHandler(bool replaceState, IState nextState);

        public override void _Ready()
        {
            stateStack = new Stack<IState>();
            Transitions = new List<Transition>();
            StateChange += OnTransition;
        }

        public void Update()
        {
            if (Transitions.Count > 0)
            {
                ResolveTransitions(Transitions);
            }
            CurrentState?.Execute();
        }

        private void ResolveTransitions(List<Transition> transition)
        {
            for (int i = 0; i < transition.Count; i++)
            {
                var transitionItem = transition[i];
                if (transitionItem != null)
                {
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
            if (stateStack == null || stateStack.Count <= 1) return;

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

        public void OnTransition(bool replaceState,  IState state = null)
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