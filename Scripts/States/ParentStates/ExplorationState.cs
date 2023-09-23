using Thrones;
using Thrones.Scripts;
using Thrones.Scripts.States;

namespace ThronesEra.Scripts.States.ParentStates
{
    public partial class ExplorationState : IState
    {
        /// data

        /// nodes

        public sealed override StateManager StateManager { get; set; }
        public sealed override GameManager GameManager { get; set; }

        /// signals

        /// methods

        public ExplorationState(StateManager stateManager, GameManager gameManager)
        {
            StateManager = stateManager;
            GameManager = gameManager;
        }

        public override void Enter()
        {
            Logger.INFO("Transitioned to Exploration State");
            GameManager.EmitSignal(GameManager.SignalName.ChangeControlledPc, GameManager.ControlledCharacter);
        }

        public override void Execute()
        {
        }

        public override void Exit()
        {
        }
    }
}