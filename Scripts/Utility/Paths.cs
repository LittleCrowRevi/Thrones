﻿namespace Thrones.Scripts.Utility;

public static class Paths
{
    public const string DEVLEVEL = "res://Scenes/Levels/dev_level.tscn";

    // sprites
    public const string REDSPRITE = "res://Resources/Sprites/red/red-sprite-sheet.png";
    public static string REDPLAYERSCENE { get; set; } = "Scenes/Entities/red.tscn";
    public static string SceneLoader { get; set; } = "Scripts/SceneLoader.cs";
    public static string GlobalCamera { get; set; } = "Scripts/GlobalCamera.cs";
    public static string StateManager { get; set; } = "Scripts/StateManager.cs";
}