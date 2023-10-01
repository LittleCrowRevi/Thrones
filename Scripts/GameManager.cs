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
    [Signal] public delegate void ChangeControlledPcEventHandler(Node2D target);

    /// nodes

    private GlobalCamera Camera { get; set; }
    private Node World { get; set; }
    private GlobalLoader GlobalLoader { get; set; }
    private HPBar HpBar { get; set; }

    /// states
    [Export] public StateManager StateManager { get; set; }

    /// Player Data

    public Node2D PlayerCharacters { get; set; }
    public IEntity ControlledCharacter { get; set; }
    private NodePath CurrentLocation { get; set; }

    /// methods
    public override void _Ready()
    {
        InitGameAsync();
    }

    public override void _Process(double delta)
    {
        if (Input.IsActionJustReleased("loadDev"))
            GlobalLoader.EmitSignal(GlobalLoader.SignalName.InitLoadScene, Paths.DEVLEVEL, true);
    }

    private void InitGameAsync()
    {
        Logger.INFO("Initializing Game");

        // Load Control Nodes
        StateManager = new StateManager();
        AddChild(StateManager);
        
        // HUD
        LoadHUD();

        // Camera
        Camera = new GlobalCamera();
        ChangeControlledPc += Camera.OnChangeTarget;
        AddChild(Camera);

        // SceneLoader
        GlobalLoader = new GlobalLoader();
        GlobalLoader.LoadingBar = (ProgressBar)GetNode("HUD/Control/ProgressBar");
        AddChild(GlobalLoader);

        // PlayerCharacters Array
        PlayerCharacters = new Node2D();
        PlayerCharacters.YSortEnabled = true;
        PlayerCharacters.Name = "PlayerCharacters";
        AddChild(PlayerCharacters);

        // Load Player Characters
        var redEntity = new RedEntity(
            new CoreStatsComponent(1, 1, 1, 1),
            new VitalStatsComponent(100, 100, 100),
            new EntityControlComponent()
        );
        
        ControlledCharacter = redEntity;
        ControlledCharacter.Visible = true;
        ControlledCharacter.Vitals.HpChange += HpBar.UpdateHpInfo;
        PlayerCharacters.AddChild(ControlledCharacter);

        EmitSignal(SignalName.ChangeControlledPc, ControlledCharacter);

        // Load Last Active Scene
        GlobalLoader.EmitSignal(GlobalLoader.SignalName.InitLoadScene, Paths.DEVLEVEL, true);

        HpBar.Visible = true;
    }

    private void LoadHUD()
    {
        HpBar = new HPBar();
        HpBar.Visible = false;
        HpBar.TextureUnder = GlobalLoader.LoadTexture(Paths.HPBARUNDER);
        HpBar.TextureProgress = GlobalLoader.LoadTexture(Paths.HPBARFILL);
        HpBar.TextureProgressOffset = new Vector2(12F, 1F);
        HpBar.Position = new Vector2(-350F, -460F);
        HpBar.Scale = new Vector2(5F, 5F);
        HpBar.Value = 100;
        
        GetNode("HUD/Control").AddChild(HpBar);
    }

    public static GameManager GetGameScript(Node node)
    {
        return node.GetTree().Root.GetNode("GameManager").GetScript().As<GameManager>();
    }
}