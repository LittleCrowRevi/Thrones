using Godot;
using System;

public partial class PlayerData : Node
{
	[Export]
	public int health;
	[Export]
	public int currentHealth;
	[Export]
	public int mana;
	[Export]
	public int currentMana;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		health = 100;
		currentHealth = 100;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
