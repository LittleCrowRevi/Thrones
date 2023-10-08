using System.Threading.Tasks;
using Godot;
using Thrones.Scripts;
using Thrones.Scripts.Utility;
using Thrones.Util;
using ThronesEra;
using ThronesEra.Scenes.HUD;
using ThronesEra.Scripts.Components;
using ThronesEra.Scripts.Entities;

namespace Thrones;

public partial class GameManager : Node2D
{
    /// signals
    [Signal] public delegate void ChangeControlledPcEventHandler(IEntity target);

    /// nodes

    public GlobalCamera Camera { get; set; }
    public Node World { get; set; }
    public GlobalLoader GlobalLoader { get; set; }
    public HPBar HpBar { get; set; }

    /// states
    [Export] public StateManager StateManager { get; set; }

    /// Player Data

    public Node2D PlayerCharacters { get; set; }
    public IEntity ControlledCharacter { get; set; }
    public NodePath CurrentLocation { get; set; }
    public HUD HUD { get; set; }

    /// methods
    public override void _Ready()
    {
        var startUp = new StartUpManager(this);
        startUp.InitGame();
    }

    public override void _Process(double delta)
    {
        if (Input.IsActionJustReleased("loadDev"))
            GlobalLoader.EmitSignal(GlobalLoader.SignalName.InitLoadScene, Paths.DEVLEVEL, true);
        if (Input.IsActionJustReleased("OpenSkillList"))
        {
            var skillList = HUD.SkillList;
            skillList.AddItem(ControlledCharacter.Skills.Skills[0].Name);
        }
    }

    public static GameManager GetGameScript(Node node)
    {
        return node.GetTree().Root.GetNode("GameManager").GetScript().As<GameManager>();
    }
}