using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thrones.Scripts;
using Thrones.Scripts.States;
using Thrones;

namespace ThronesEra.Scripts.States.ParentStates
{
    public partial class ExplorationState : IState
    {
        /// data

        /// nodes

        public Node ControlledPC { get; set; }
        public Camera2D GlobalCamera { get; set; }
        public override StateManager StateManager { get; set;  }
        public override GameManager GameManager { get; set; }

        /// signals

        /// methods

        public ExplorationState(StateManager stateManager, Node controlledPC, Camera2D globalCamera)
        {
            StateManager = stateManager;
            ControlledPC = controlledPC;
            GlobalCamera = globalCamera;
            GameManager.ChangeControlledPC += OnChangeControlledCharacter;
        }

        private void OnChangeControlledCharacter(Node target)
        {
            ControlledPC = target;
            GameManager.EmitSignal(GameManager.SignalName.ChangeControlledPC, ControlledPC);
        }

        public override void Enter()
        {
            throw new NotImplementedException();
        }

        public override void Execute()
        {
            throw new NotImplementedException();
        }

        public override void Exit()
        {
            throw new NotImplementedException();
        }
    }
}
