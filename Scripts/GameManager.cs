using Godot;
using System.Threading.Tasks;
using System.Web;
using Thrones.Scripts;
using Thrones.Scripts.Utility;
using Thrones.Util;
using ThronesEra;

namespace Thrones;

public partial class GameManager : Node2D
{
    /// nodes

    [Export] public GlobalCamera Camera { get; set; }
    [Export] public Node World { get; set; }
    [Export] public SceneLoader SceneLoader { get; set; }

    /// signals

    [Signal] public delegate void ChangeControlledPcEventHandler(Node2D target);

    /// states

    [Export] public StateManager StateManager { get; set; }

    /// Player Data

    public Node2D ControlledCharacter { get; set; }
    public Node2D PlayerCharacters { get; set; }
    public NodePath CurrentLocation { get; set; }

    /// methods

    public override async void _Ready()
    {
        await InitGameAsync();
    }

    public override void _Process(double delta)
    {
    }

    private async Task InitGameAsync()
    {
        Logger.INFO("Initializing Game");

        // Load Control Nodes
        StateManager = new StateManager();
        AddChild(StateManager);

        // Camera
        Camera = new GlobalCamera();
        ChangeControlledPc += Camera.OnChangeTarget;
        AddChild(Camera);

        // SceneLoader
        SceneLoader = new SceneLoader();
        AddChild(SceneLoader);

        // PlayerCharacters Array
        PlayerCharacters = new Node2D();
        PlayerCharacters.Name = "PlayerCharacters";
        AddChild(PlayerCharacters);

        // Load Player Characters
        var player = await SceneLoader.LoadEntity(Paths.RedPlayer);
        ControlledCharacter = (Node2D)((PackedScene)player).Instantiate();
        PlayerCharacters.AddChild(ControlledCharacter);

        EmitSignal(SignalName.ChangeControlledPc, ControlledCharacter);

        // Load Last Active Scene
        SceneLoader.EmitSignal(SceneLoader.SignalName.InitLoadScene, Paths.DevLevel, true);
            
            
    }

    public static GameManager GetGameScript(Node node)
    {
        return node.GetTree().Root.GetNode("GameManager").GetScript().As<GameManager>();
    }
}