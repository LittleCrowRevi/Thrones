using System;
using System.Collections.Generic;
using System.Linq;
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

    [Signal] public delegate void HealthChangeEventHandler(int currentHp, int totalHp);
    [Signal] public delegate void ManaChangeEventHandler(int currentHp, int totalHp);

    #region Data Fields
    
    public IEntity ParentEntity { get; set; }

    public int TotalHp { get; set; } = 100;
    public int CurrentHp { get; set; }

    public int TotalMp { get; set; } = 100;
    public int CurrentMp { get; set; }

    public int TotalEp { get; set; } = 100;
    public int CurrentEp { get; set; }

    #endregion

    #region Methods

    public void CalculateHealth()
    {
        var components = (CoreStatsComponent)ParentEntity.QueryComponents()[1];
        TotalHp = components.Constitution * 100;
        EmitSignal(SignalName.HealthChange, CurrentHp, TotalHp);
    }
    
    public void CalculateMana()
    {
        var component = (CoreStatsComponent)ParentEntity.QueryComponents().ToList()[1];
        TotalMp = component.Intelligence * 100;
        EmitSignal(SignalName.ManaChange, CurrentMp, TotalMp);
    }

    public void OnHeal(int heal)
    {
        var newHp = Math.Clamp(CurrentHp + heal, 0, TotalHp);
        CurrentHp = newHp;
        EmitSignal(SignalName.HealthChange, CurrentHp, TotalHp);
    }
    
    public void OnDamage(int damage)
    {
        var newHp = Math.Clamp(CurrentHp - damage, 0, TotalHp);
        CurrentHp = newHp;
        EmitSignal(SignalName.HealthChange, CurrentHp, TotalHp);
    }
    
    #endregion
}