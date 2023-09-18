using Godot;

namespace Thrones.Scripts.Utility
{
    public static class Paths
    {
        public static NodePath RedPlayer { get; set; } = new NodePath("res://Scenes/Entities/red.tscn");
        public static NodePath SceneLoader { get; set; } = new NodePath("res://Scripts/SceneLoader.cs");
        public static NodePath GlobalCamera { get; set; } = new NodePath("res://Scripts/GlobalCamera.cs");
        public static NodePath StateManager { get; set; } = new NodePath("res://Scripts/StateManager.cs");
    }
}