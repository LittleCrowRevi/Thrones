﻿using System.Collections.Generic;
using Godot;
using ThronesEra.Scripts.Entities.Components;

namespace ThronesEra.Scripts.Entities;

public partial class IEntity : CharacterBody2D
{
    public virtual CoreStatsComponent CoreStats { get; set; }
    public virtual VitalStatsComponent Vitals { get; set; }

    public virtual IEnumerable<Component> QueryComponents()
    {
        var components = new List<Component>
        {
            Vitals,
            CoreStats
        };
        return components;
    }
    
}