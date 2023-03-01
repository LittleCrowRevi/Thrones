using Godot;
using System;

public partial class hud : CanvasLayer
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// connect the health change signal
		var player = GetNode<Player>("/root/Node2D/Player");
		player.HealthChanged += _on_player_health_changed; 
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	// function which controls the hp progress bar and the damage text(?)
	public void _on_player_health_changed(int changeValue)
	{
		var hpProgressBar = (TextureProgressBar)GetNode("HP-Bar");
		hpProgressBar.Value += changeValue;

		GD.Print($"[INFO] HP changed by {changeValue}");
	}
}
