namespace Thrones.Scripts.Utility;

public static class Paths
{
    public static string SceneLoader { get; set; } = "Scripts/SceneLoader.cs";
    public static string GlobalCamera { get; set; } = "Scripts/GlobalCamera.cs";
    public static string StateManager { get; set; } = "Scripts/StateManager.cs";

    // sprites
    public const string REDSPRITE = "res://Resources/Sprites/red/red-sprite-sheet.png";
    public const string DEVLEVEL = "res://Scenes/Levels/dev_level.tscn";
    public static string REDPLAYERSCENE { get; set; } = "Scenes/Entities/red.tscn";
    public const string HPBARFILL = "res://Resources/HUD Textures/hp-barfill.png";
    public const string HPBARUNDER = "res://Resources/HUD Textures/hp-barframe.png";
}