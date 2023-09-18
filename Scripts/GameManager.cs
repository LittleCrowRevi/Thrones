using Godot;
using Thrones.Scripts;
using Thrones.Scripts.Utility;
using Thrones.Util;
using ThronesEra;
using ThronesEra.Scripts.States.ParentStates;

namespace Thrones
{
    public partial class GameManager : Node2D
    {
        /// nodes

        [Export] public Camera2D Camera { get; set; }
        [Export] public Node World { get; set; }
        [Export] public SceneLoader sceneLoader { get; set; }

        /// signals

        [Signal] public delegate void ChangeControlledPCEventHandler(Node target);

        /// states

        [Export] public StateManager stateManager { get; set; }

        /// Player Data

        public Node ControlledCharacter { get; set; }
        public Node[] Party { get; set; }
        public NodePath CurrentLocation { get; set; }

        /// methods

        public override void _Ready()
        {
            InitGame();
        }

        public override void _Process(double delta)
        {
        }

        private async void InitGame()
        {
            Logger.INFO("Initializing Game");

            // Load Player Characters
            var player = sceneLoader.LoadEntity(Paths.RedPlayer);

            // Load Last Active Scene
            sceneLoader.EmitSignal(SceneLoader.SignalName.InitLoadScene, "res://Scenes/Levels/dev_level.tscn", true);
            ChangeControlledPC += Camera.GetScript().As<GlobalCamera>().OnChangeTarget;

            ExplorationState exState = new(stateManager, this);
            stateManager.InitialState(exState);
        }

        public static GameManager GetGameScript(Node node)
        {
            return node.GetTree().Root.GetNode("GameManager").GetScript().As<GameManager>();
        }
    }
}