using System.Collections.Generic;
using Godot;
using Thrones.Scripts.Utility;
using Thrones.Util;
using ThronesEra.Scripts.Entities.Components;
using ThronesEra.Scripts.Components;

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

        _sprite2D = CreateSprite(Paths.REDSPRITE);
        AddChild(_sprite2D);
        
        Name = "Red";
        Position = new Vector2(40f, 50f);
        YSortEnabled = true;

    }

    public CoreStatsComponent CoreStats { get; set; }
    public VitalStatsComponent Vitals { get; set; }
    public EntityControlComponent EntityControlComponent { get; set; }

    private Sprite2D _sprite2D;

    public IEnumerable<Component> QueryComponents()
    {
        List<Component> components = new()
        {
            Vitals,
            CoreStats
        };
        return components;
    }

    private Sprite2D CreateSprite(string path)
    {
        var texture = GlobalLoader.LoadTexture(path);
        var sprite2D = new Sprite2D();
        sprite2D.Name = "Sprite2D";
        sprite2D.Texture = texture;
        sprite2D.Hframes = 16;
        sprite2D.Frame = 4;
        sprite2D.Scale = new Vector2(1.1F, 1.1F);
        return sprite2D;
    }
}