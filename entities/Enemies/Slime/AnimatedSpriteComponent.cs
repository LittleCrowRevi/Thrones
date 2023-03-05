using Godot;
using System;

public partial class AnimatedSpriteComponent : AnimatedSprite2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		if (this.SpriteFrames.HasAnimation("idle"))
		{
			Animation = "idle";
			Play();
		} else {
			Animation = this.SpriteFrames.Animations[0].ToString();
			GD.Print($"[Info] {this.SpriteFrames.Animations[0].ToString()}");
			Play();
		}

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
