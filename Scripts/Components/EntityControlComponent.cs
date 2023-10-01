using System;
using Godot;

namespace ThronesEra.Scripts.Components;

public partial class EntityControlComponent : Node
{
    /// Data
    [Export] public float Speed { get; set; } = 200.0f;

    public bool CanMove { get; set; }

    /// Nodes
    private Sprite2D _sprite;
    private AnimationTree _animTree;
    private CharacterBody2D _entity;

    /// Methods

    public EntityControlComponent()
    {
        Name = "EntityControlComponent";
    }
    
    public override void _Ready()
    {
        
        _entity = GetParent<CharacterBody2D>();
        _sprite = _entity.GetNode<Sprite2D>("Sprite2D");

        _animTree = _entity.GetNode<AnimationTree>("AnimationTree");
        _animTree?.Set("parameters/conditions/idle", (_entity.Velocity == new Vector2(0, 0)));

        CanMove = true;
    }

    public override void _PhysicsProcess(double delta)
    {
        Movement();
        _entity.MoveAndSlide();
    }


    private void Movement()
    {
        if (!CanMove)
        {
            return;
        }
        var stateMachine = (AnimationNodeStateMachinePlayback)_animTree?.Get("parameters/playback");

        Vector2 velocity;
        var direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");

        // Changes the walking animation based on velocity input and
        // calculates velocity
        if (direction != Vector2.Zero)
        {
            // Calculates the walking velocity
            velocity = direction.Normalized() * Speed;

            // set the walking animation
            _animTree?.Set("parameters/Walk/blend_position", velocity);
            stateMachine?.Travel("Walk");

            // if there is no walk input switches to idle
        }
        else
        {
            velocity = Vector2.Zero;
            stateMachine?.Travel("Idle");
        }

        _entity.Velocity = velocity;
    }
}