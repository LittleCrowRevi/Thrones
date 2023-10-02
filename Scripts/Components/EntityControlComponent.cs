using Godot;
using Thrones.Scripts;
using ThronesEra.Scripts.Entities;
using ThronesEra.Scripts.States.ParentStates;

namespace ThronesEra.Scripts.Components;

public partial class EntityControlComponent : Node
{
    private AnimationTree _animTree;
    private CharacterBody2D _entity;
    private StateManager _stateManager;
    
    /// Nodes
    private Sprite2D _sprite;

    /// Methods
    public EntityControlComponent(StateManager stateManager)
    {
        _stateManager = stateManager;
        Name = "EntityControlComponent";
    }

    /// Data
    [Export]
    public float Speed { get; set; } = 200.0f;
    public bool CanMove { get; set; }

    public override void _PhysicsProcess(double delta)
    {
        if (_stateManager.CurrentState is not ExplorationState) return;
        Movement();
        _entity.MoveAndSlide();
    }
    
    public void OnChangeControlledPc(IEntity entity)
    {
        if (GetParent() is not null) RemoveChild(this);
        entity.AddChild(this);
        _entity = entity;
        _sprite = _entity.GetNode<Sprite2D>("Sprite2D");

        _animTree = _entity.GetNode<AnimationTree>("AnimationTree");
        _animTree?.Set("parameters/conditions/idle", _entity.Velocity == new Vector2(0, 0));

        CanMove = true;
    }

    private void Movement()
    {
        if (!CanMove) return;
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