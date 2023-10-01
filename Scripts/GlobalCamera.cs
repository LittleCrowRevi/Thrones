using System;
using Godot;
using ThronesEra;

namespace Thrones.Scripts;

public partial class GlobalCamera : Camera2D
{
    public GlobalCamera()
    {
        Name = "GlobalCamera";
        PositionSmoothingEnabled = true;
        RotationSmoothingEnabled = true;
        ProcessCallback = Camera2DProcessCallback.Physics;
    }
    
    /// data
    private Node2D _target;
    private Vector2 _zoomLevels = new(2.5f, 5f);

    private Node2D Target
    {
        get => _target;
        set
        {
            Logger.INFO("setting target for camera");
            _target = value;
        }
    }

    /// Methods
    public override void _Ready()
    {
        Zoom = new Vector2(3f, 3f);
        MakeCurrent();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _PhysicsProcess(double delta)
    {
        if (Target != null) Position = Target.Position;
        CameraZoom();
    }

    public void OnChangeTarget(Node2D newTarget)
    {
        Logger.INFO("changing camera target");
        Target = newTarget;
        Position = Target.Position;
    }

    /// <summary>
    ///     Handles Camera Zoom as well as limits of the zoom.
    /// </summary>
    private void CameraZoom()
    {
        // Zoom Action
        if (Input.IsActionJustReleased("scroll_up"))
        {
            var clampedZoom = Math.Clamp(Zoom.X * 1.05f, _zoomLevels.X, _zoomLevels.Y);
            Zoom = new Vector2(clampedZoom, clampedZoom);
        }

        if (Input.IsActionJustReleased("scroll_down"))
        {
            var clampedZoom = Math.Clamp(Zoom.X * 0.95f, _zoomLevels.X, _zoomLevels.Y);
            Zoom = new Vector2(clampedZoom, clampedZoom);
        }
    }
}