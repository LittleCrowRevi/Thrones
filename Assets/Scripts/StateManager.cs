using Assets.Scripts.States;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts
{
    // implement transition methods? from to?
    public class StateManager
    {
        // Events
        public delegate void NotifyEvent(bool replaceState, IState nextState);
        public event NotifyEvent NotifyStateChange;
        //public UnityEvent<bool, IState> NotifyStateChange { get; set; }

        // Data Fields
        private readonly Stack<IState> stateStack;
        public IState CurrentState => stateStack?.Peek();
        private readonly List<Transitions> Transition;

        // Events
        public UnityEvent PopState { get; set; }


        public StateManager() 
        { 
            stateStack = new Stack<IState>();
            Transition = new List<Transitions>();
            NotifyStateChange += OnTransition;
        }

        public void Update()
        {
            if (Transition.Count > 0)
            {
                ResolveTransitions(Transition);
            }
            CurrentState?.Execute();
        }

        private void ResolveTransitions(List<Transitions> transition)
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
                            Transition.Remove(transitionItem);
                            break;
                        case false:
                            AddState(transitionItem.Next);
                            Transition.Remove(transitionItem);
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
            var t = new Transitions(state, replaceState);
            Transition.Add(t);
        }

        private sealed class Transitions
        {
            public IState Next;
            public bool replaceState;

            public Transitions(IState next, bool replaceState)
            {
                Next = next;
                this.replaceState = replaceState;
            }
        }
    }

}