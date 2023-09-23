using Godot;
using ThronesEra;

namespace Thrones.Scripts
{
    public partial class GlobalCamera : Camera2D
    {
        /// data
        private Node2D _Target;

        private Node2D Target
        {
            get { return _Target; }
            set
            {
                Logger.INFO("setting target for camera");
                _Target = value;
            }
        }

        private Vector2 _zoomLevels = new(2.5f, 2.5f);

        /// signals

        /// Methods

        public GlobalCamera()
        {
            Name = "GlobalCamera";
            PositionSmoothingEnabled = true;
            RotationSmoothingEnabled = true;
            ProcessCallback = Camera2DProcessCallback.Physics;
            Zoom = _zoomLevels;
        }

        public override void _Ready()
        {
            MakeCurrent();
        }

        // Called every frame. 'delta' is the elapsed time since the previous frame.
        public override void _PhysicsProcess(double delta)
        {
            if (Target != null)
            {
                Position = Target.Position;
            }
        }

        public void OnChangeTarget(Node2D newTarget)
        {
            Logger.INFO("changing camera target");
            Target = newTarget;
            Position = Target.Position;
        }
    }
}