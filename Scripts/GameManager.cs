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

        [Export] public GlobalCamera Camera { get; set; }
        [Export] public Node World { get; set; }
        [Export] public SceneLoader sceneLoader { get; set; }

        /// signals

        [Signal] public delegate void ChangeControlledPCEventHandler(Node2D target);

        /// states

        [Export] public StateManager stateManager { get; set; }

        /// Player Data

        public Node2D ControlledCharacter { get; set; }
        public Node2D PlayerCharacters { get; set; }
        public NodePath CurrentLocation { get; set; }

        /// methods

        public override void _Ready()
        {
            InitGame();
        }

        public override void _Process(double delta)
        {
            if (ControlledCharacter != null)
            {
            }
        }

        private async void InitGame()
        {
            Logger.INFO("Initializing Game");

            // Load Control Nodes
            stateManager = new StateManager();
            AddChild(stateManager);

            Camera = new GlobalCamera();
            ChangeControlledPC += Camera.OnChangeTarget;
            AddChild(Camera);

            sceneLoader = new SceneLoader();
            AddChild(sceneLoader);

            PlayerCharacters = new Node2D();
            AddChild(PlayerCharacters);

            // Load Player Characters
            var player = await SceneLoader.LoadEntity(Paths.RedPlayer);
            ControlledCharacter = (Node2D)((PackedScene)player).Instantiate();
            PlayerCharacters.AddChild(ControlledCharacter);
            
            EmitSignal(SignalName.ChangeControlledPC, ControlledCharacter);

            // Load Last Active Scene
            sceneLoader.EmitSignal(SceneLoader.SignalName.InitLoadScene, "res://Scenes/Levels/dev_level.tscn", true);

            // TODO: fix states
            //ExplorationState exState = new(stateManager, this);
            //stateManager.InitialState(exState);
        }

        public static GameManager GetGameScript(Node node)
        {
            return node.GetTree().Root.GetNode("GameManager").GetScript().As<GameManager>();
        }
    }
}