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
                Logger.INFO("setting target");
                _Target = value;
            }
        }

        /// signals

        /// Methods

        public GlobalCamera()
        {
            Name = "GlobalCamera";
        }
        public override void _Ready()
        {
            MakeCurrent();
            
        }

        // Called every frame. 'delta' is the elapsed time since the previous frame.
        public override void _Process(double delta)
        {

        }

        public void OnChangeTarget(Node2D newTarget)
        {
            Logger.INFO("Changing Camera Target");
            Target = newTarget;
            
            Logger.INFO(Target.Position.ToString());
        }
    }
}