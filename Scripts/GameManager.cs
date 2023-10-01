using System.Threading.Tasks;
using Godot;
using Thrones.Scripts;
using Thrones.Scripts.Utility;
using Thrones.Util;
using ThronesEra;
using ThronesEra.Scripts.Components;
using ThronesEra.Scripts.Entities;
using ThronesEra.Scripts.Entities.Components;

namespace Thrones;

public partial class GameManager : Node2D
{
    /// signals
    [Signal]
    public delegate void ChangeControlledPcEventHandler(Node2D target);

    /// nodes

    [Export] public GlobalCamera Camera { get; set; }
    [Export] public Node World { get; set; }
    [Export] public GlobalLoader GlobalLoader { get; set; }

    /// states

    [Export] public StateManager StateManager { get; set; }

    /// Player Data

    public Node2D ControlledCharacter { get; set; }
    public Node2D PlayerCharacters { get; set; }
    public NodePath CurrentLocation { get; set; }

    /// methods
    public override void _Ready()
    {
        InitGameAsync();
    }

    public override void _Process(double delta)
    {
        if (Input.IsActionJustReleased("loadDev"))
        {
            GlobalLoader.EmitSignal(GlobalLoader.SignalName.InitLoadScene, Paths.DEVLEVEL, true);
        }
    }

    private void InitGameAsync()
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
        GlobalLoader = new GlobalLoader();
        GlobalLoader.loadingBar = (ProgressBar)GetNode("CanvasLayer/Control/ProgressBar");
        AddChild(GlobalLoader);

        // PlayerCharacters Array
        PlayerCharacters = new Node2D();
        PlayerCharacters.YSortEnabled = true;
        PlayerCharacters.Name = "PlayerCharacters";
        AddChild(PlayerCharacters);

        // Load Player Characters
        ControlledCharacter = new RedEntity(
            new CoreStatsComponent(1, 1, 1, 1),
            new VitalStatsComponent(1, 1, 1),
            new EntityControlComponent()
            );
        ControlledCharacter.Visible = true;
        PlayerCharacters.AddChild(ControlledCharacter);

        EmitSignal(SignalName.ChangeControlledPc, ControlledCharacter);

        // Load Last Active Scene
        GlobalLoader.EmitSignal(GlobalLoader.SignalName.InitLoadScene, Paths.DEVLEVEL, true);
    }

    public static GameManager GetGameScript(Node node)
    {
        return node.GetTree().Root.GetNode("GameManager").GetScript().As<GameManager>();
    }
}