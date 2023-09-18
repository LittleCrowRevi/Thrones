using Godot;
using System;

namespace Thrones.Components
{
    public partial class EntityControlComponent : Node
    {
        /// Data

        // Movement Speed
        [Export] public float Speed = 300.0f;

        // Min and Max Zoom
        private Vector2 ZoomLevels = new(2.5f, 5.0f);

        public bool CanMove { get; set; }

        /// Nodes

        private Camera2D Camera;
        private Sprite2D Sprite;
        private AnimationTree AnimTree;
        private CharacterBody2D Entity;

        /// Methods

        public override void _Ready()
        {
            Viewport root = GetTree().Root;
            Camera = root.GetNode<Node>("GameManager").GetNode<Camera2D>("GlobalCamera");

            Entity = GetParent<CharacterBody2D>();
            Sprite = Entity.GetNode<Sprite2D>("Sprite2D");

            AnimTree = Entity.GetNode<AnimationTree>("AnimationTree");
            AnimTree?.Set("parameters/conditions/idle", (Entity.Velocity == new Vector2(0, 0)));

            CanMove = true;
        }

        public override void _Process(double delta)
        {
            CameraZoom();
        }

        public override void _PhysicsProcess(double delta)
        {
            Movement();
            Entity.MoveAndSlide();
        }

        public void CameraZoom()
        {
            // Zoom Action
            if (Input.IsActionJustReleased("scroll_up"))
            {
                Vector2 currentZoom = Camera.Zoom;
                float clampedZoom = Math.Clamp(currentZoom.X * 1.1f, ZoomLevels.X, ZoomLevels.Y);
                Camera.Zoom = new Vector2(clampedZoom, clampedZoom);
            }
            if (Input.IsActionJustReleased("scroll_down"))
            {
                Vector2 currentZoom = Camera.Zoom;
                float clampedZoom = Math.Clamp(currentZoom.X * 0.8f, ZoomLevels.X, ZoomLevels.Y);
                Camera.Zoom = new Vector2(clampedZoom, clampedZoom);
            }
        }

        public void Movement()
        {
            if (!CanMove)
            {
                return;
            }
            AnimationNodeStateMachinePlayback stateMachine = (AnimationNodeStateMachinePlayback)AnimTree?.Get("parameters/playback");

            Vector2 velocity = Entity.Velocity;
            Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");

            // Changes the walking animation based on velocity input and
            // calculates velocity
            if (direction != Vector2.Zero)
            {
                // Calculates the walking velocity
                velocity = direction.Normalized() * Speed;

                // set the walking animation
                AnimTree?.Set("parameters/Walk/blend_position", velocity);
                stateMachine?.Travel("Walk");

                // if there is no walk input switches to idle
            }
            else
            {
                velocity = Vector2.Zero;
                stateMachine?.Travel("Idle");
            }

            Entity.Velocity = velocity;
        }
    }
}