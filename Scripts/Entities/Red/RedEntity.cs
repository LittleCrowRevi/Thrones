using System.Collections.Generic;
using Godot;
using Thrones.Components;
using ThronesEra.Scripts.Entities.Components;

namespace ThronesEra.Scripts.Entities;

public partial class RedEntity : CharacterBody2D, IEntity
{
    public RedEntity(CoreStatsComponent coreStats, VitalStatsComponent vitals, EntityControlComponent entityControlComponent)
    {
        CoreStats = coreStats;
        AddChild(CoreStats);
        Vitals = vitals;
        AddChild(Vitals);
        EntityControlComponent = entityControlComponent;
        AddChild(EntityControlComponent);
        
        Name = "Red";
        Position = new Vector2(40f, 50f);
        YSortEnabled = true;

    }

    public CoreStatsComponent CoreStats { get; set; }
    public VitalStatsComponent Vitals { get; set; }
    public EntityControlComponent EntityControlComponent { get; set; }

    public IEnumerable<Component> QueryComponents()
    {
        List<Component> components = new()
        {
            Vitals,
            CoreStats
        };
        return components;
    }
}