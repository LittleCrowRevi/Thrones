using System.Collections.Generic;
using Godot;
using ThronesEra.Scripts.Entities.Components;

namespace ThronesEra.Scripts.Entities;

public partial class RedEntity : CharacterBody2D, IEntity
{
    public CoreStatsComponent CoreStats { get; set; }
    public VitalStatsComponent Vitals { get; set; }

    public IEnumerable<Component> QueryComponents()
    {
        List<Component> components = new()
        {
            Vitals,
            CoreStats
        };
        return components;
    }

    public override void _Ready()
    {
        Name = "Red";
        Position = new Vector2(40f, 50f);
    }
}