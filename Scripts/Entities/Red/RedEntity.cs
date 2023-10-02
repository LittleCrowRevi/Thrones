using System.Collections.Generic;
using Godot;
using Thrones.Scripts.Utility;
using Thrones.Util;
using ThronesEra.Scripts.Components;
using ThronesEra.Scripts.Entities.Components;

namespace ThronesEra.Scripts.Entities;

public partial class RedEntity : IEntity
{
    public RedEntity(CoreStatsComponent coreStats, VitalStatsComponent vitals)
    {
        CoreStats = coreStats;
        AddChild(CoreStats);
        Vitals = vitals;
        AddChild(Vitals);
        
        _sprite2D = CreateSprite(Paths.REDSPRITE);
        AddChild(_sprite2D);

        Name = "Red";
        YSortEnabled = true;
    }

    private readonly Sprite2D _sprite2D;
    public sealed override CoreStatsComponent CoreStats { get; set; }
    public sealed override VitalStatsComponent Vitals { get; set; }
    public EntityControlComponent EntityControlComponent { get; set; }

    public override IEnumerable<Component> QueryComponents()
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

    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("dev_heal"))
        {
            Vitals.OnHeal(10);
        }
        if (Input.IsActionJustPressed("dev_damage"))
        {
            Vitals.OnDamage(10);
        }
    }
}