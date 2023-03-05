using Godot;
using System;

public partial class Slime : CharacterBody2D
{
	public const float Speed = 300.0f;
	public Godot.AnimationTree AnimTree;
	private Vector2 velocity = new Vector2(0, 0);

	public override void _Ready()
	{
		AnimTree = GetNode<Godot.AnimationTree>("AnimationTree");
		AnimTree.Set("parameters/Idle/blend_position", velocity);
	}


	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

	}
}
