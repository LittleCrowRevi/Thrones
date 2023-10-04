using System.Threading.Tasks;
using Godot;
using Thrones.Scripts;
using Thrones.Scripts.Utility;
using Thrones.Util;
using ThronesEra;
using ThronesEra.Scenes.HUD;
using ThronesEra.Scripts.Components;
using ThronesEra.Scripts.Entities;
using ThronesEra.Scripts.Entities.Components;
using ThronesEra.Scripts.States.ParentStates;

namespace Thrones;

public partial class GameManager : Node2D
{
    /// signals
    [Signal] public delegate void ChangeControlledPcEventHandler(IEntity target);

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
    private HUD HUD { get; set; }

    /// methods
    public override void _Ready()
    {
        InitGame();
    }

    public override void _Process(double delta)
    {
        if (Input.IsActionJustReleased("loadDev"))
            GlobalLoader.EmitSignal(GlobalLoader.SignalName.InitLoadScene, Paths.DEVLEVEL, true);
    }

    private void InitGame()
    {
        Logger.INFO("Initializing Game");

        // Load Control Nodes
        StateManager = new StateManager();
        AddChild(StateManager);
        
        // SceneLoader
        GlobalLoader = new GlobalLoader();
        AddChild(GlobalLoader);
        
        // HUD
        LoadHUD();

        // Camera
        Camera = new GlobalCamera();
        ChangeControlledPc += Camera.OnChangeTarget;
        AddChild(Camera);
        
        // Load Last Active Scene
        GlobalLoader.EmitSignal(GlobalLoader.SignalName.InitLoadScene, Paths.DEVLEVEL, true);
        
        // input
        var entityControl = new EntityControlComponent(StateManager);
        ChangeControlledPc += entityControl.OnChangeControlledPc;
        
        // Player Chars
        LoadPlayer();
        
        StateManager.EmitSignal(StateManager.SignalName.StateChange, true, new ExplorationState(StateManager, this));
        Logger.INFO("Initiated Game");
        HpBar.Visible = true;
    }

    private void LoadHUD()
    {
        HUD = new HUD();
        AddChild(HUD);
        HUD.StateManager = StateManager;
        GlobalLoader.LoadingBar = HUD.LoadingBar;
        
        HpBar = new HPBar();
        HpBar.Visible = false;
        HpBar.TextureUnder = GlobalLoader.LoadTexture(Paths.HPBARUNDER);
        HpBar.TextureProgress = GlobalLoader.LoadTexture(Paths.HPBARFILL);
        HpBar.TextureProgressOffset = new Vector2(12F, 1F);
        HpBar.Position = new Vector2(10F, 15.5F);
        HpBar.Scale = new Vector2(5F, 5F);
        HpBar.Value = 100;
        
        GetNode("HUD").AddChild(HpBar);
    }
    
    private void LoadPlayer()
    {
        // PlayerCharacters Array
        PlayerCharacters = new Node2D();
        PlayerCharacters.YSortEnabled = true;
        PlayerCharacters.Name = "PlayerCharacters";
        AddChild(PlayerCharacters);

        // Load Player Characters
        var redEntity = new RedEntity(
            new CoreStatsComponent(10, 10, 10, 10),
            new VitalStatsComponent(100, 100, 100)
        );
        ControlledCharacter = redEntity;
        ControlledCharacter.Visible = true;
        ControlledCharacter.Vitals.HealthChange += HpBar.UpdateHpInfo;
        ControlledCharacter.Vitals.ManaChange += HUD.UpdateMana;
        PlayerCharacters.AddChild(ControlledCharacter);
    }

    public static GameManager GetGameScript(Node node)
    {
        return node.GetTree().Root.GetNode("GameManager").GetScript().As<GameManager>();
    }
}