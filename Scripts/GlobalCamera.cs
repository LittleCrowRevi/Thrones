using Godot;

namespace Thrones.Scripts
{
    public partial class GlobalCamera : Camera2D
    {
        /// data

        private Node Target { get; set; }

        /// signals

        [Signal] public delegate void ChangeTargetEventHandler(Node newTarget);

        // Methods
        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            ChangeTarget += OnChangeTarget;
        }

        // Called every frame. 'delta' is the elapsed time since the previous frame.
        public override void _Process(double delta)
        {
            Position = (Vector2)Target.Get(PropertyName.Transform);
        }

        public void OnChangeTarget(Node newTarget)
        {
            Target = newTarget;
            Position = (Vector2)Target.Get(PropertyName.Transform);
        }
    }
}