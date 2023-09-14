using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.States
{
    public class ExplorationState : IState
    {

        // Data Fields
        readonly string _Name = "ExplorationState";
        public string Name { get => _Name; }
        public StateManager StateManager { get; set; }

        // Methods

        public ExplorationState(StateManager stateManager)
        {
            StateManager = stateManager;
        }

        public void Enter()
        {
            Debug.Log("[ExplorationState]");
        }

        public void Execute()
        {
        }

        public void Exit()
        {
        }
    }
}
