using System;
using Godot;

namespace ThronesEra.Scripts.Entities.Components;

public partial class VitalStatsComponent : Component
{
    public VitalStatsComponent(int currentHp, int currentMp, int currentEp)
    {
        Name =  "Vitals";
        CurrentHp = currentHp;
        CurrentMp = currentMp;
        CurrentEp = currentEp;
    }

    [Signal] public delegate void HpChangeEventHandler(int currentHp, int totalHp);

    #region Data Fields

    public int TotalHp { get; set; } = 100;
    [Export] public int CurrentHp { get; set; }

    public int TotalMp { get; set; } = 100;
    public int CurrentMp { get; set; }

    public int TotalEp { get; set; } = 100;
    public int CurrentEp { get; set; }

    #endregion

    public override void _Process(double delta)
    {
        
    }

    public void OnHeal(int heal)
    {
        var newHp = Math.Clamp(CurrentHp + heal, 0, TotalHp);
        CurrentHp = newHp;
        EmitSignal(SignalName.HpChange, CurrentHp, TotalHp);
    }
    
    public void OnDamage(int damage)
    {
        var newHp = Math.Clamp(CurrentHp - damage, 0, TotalHp);
        CurrentHp = newHp;
        EmitSignal(SignalName.HpChange, CurrentHp, TotalHp);
    }
}