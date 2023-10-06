using Godot;
using Thrones.Util;
using ThronesEra;
using ThronesEra.Scenes.HUD;
using ThronesEra.Scripts.Components;
using ThronesEra.Scripts.Entities;
using ThronesEra.Scripts.Entities.Components;
using ThronesEra.Scripts.States.ParentStates;

namespace Thrones.Scripts.Utility;

public class StartUpManager
{
    public StartUpManager(GameManager gm)
    {
        GM = gm;
    }
    private GameManager GM { get; set; }
    
    public void InitGame()
    {
        Logger.INFO("Initializing Game");

        // Load Control Nodes
        GM.StateManager = new StateManager();
        GM.AddChild(GM.StateManager);
        
        // SceneLoader
        GM.GlobalLoader = new GlobalLoader();
        GM.AddChild(GM.GlobalLoader);
        
        // HUD
        LoadHUD();

        // Camera
        GM.Camera = new GlobalCamera();
        // connect signal to camera method for changing the camera follow target
        GM.ChangeControlledPc += GM.Camera.OnChangeTarget;
        GM.AddChild(GM.Camera);
        
        // Load Last Active Scene
        GM.GlobalLoader.EmitSignal(GlobalLoader.SignalName.InitLoadScene, Paths.DEVLEVEL, true);
        
        // input
        var entityControl = new EntityControlComponent(GM.StateManager);
        GM.ChangeControlledPc += entityControl.OnChangeControlledPc;
        
        // Player Chars
        LoadPlayer();
        
        GM.StateManager.EmitSignal(StateManager.SignalName.StateChange, (int)TransitionType.Replace, new ExplorationState(GM.StateManager, GM));
        Logger.INFO("Initiated Game");
        GM.HpBar.Visible = true;
    }

    private void LoadHUD()
    {
        GM.HUD = new HUD
        {
            StateManager = GM.StateManager,
        };
        GM.GlobalLoader.LoadingBar = GM.HUD.LoadingBar;
        GM.AddChild(GM.HUD);
        

        GM.HpBar = new HPBar
        {
            Visible = false,
            TextureUnder = GlobalLoader.LoadTexture(Paths.HPBARUNDER),   
            TextureProgress = GlobalLoader.LoadTexture(Paths.HPBARFILL), 
            TextureProgressOffset = new Vector2(12F, 1F),               
            Position = new Vector2(10F, 15.5F),                         
            Scale = new Vector2(5F, 5F),                                 
            Value = 100                                                
            
        };
        
        GM.GetNode("HUD").AddChild(GM.HpBar);
    }
    
    private void LoadPlayer()
    {
        // PlayerCharacters Array
        GM.PlayerCharacters = new Node2D()
        {
            YSortEnabled = true,
            Name = "PlayerCharacters"
        };
        GM.AddChild(GM.PlayerCharacters);

        // Load Player Characters
        var redEntity = new RedEntity(
            new CoreStatsComponent(10, 10, 10, 10),
            new VitalStatsComponent(100, 100, 100)
        );
        GM.ControlledCharacter = redEntity;
        GM.ControlledCharacter.Visible = true;
        
        // connect player signals to hud
        GM.ControlledCharacter.Vitals.HealthChange += GM.HpBar.UpdateHpInfo;
        GM.ControlledCharacter.Vitals.ManaChange += GM.HUD.UpdateMana;
        GM.PlayerCharacters.AddChild(GM.ControlledCharacter);
    }
}