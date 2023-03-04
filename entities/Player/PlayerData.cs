using Godot;
using System;

public partial class PlayerData : Resource
{
	[Export] public int MaxHealth { get; set; }
	[Export] public int Health { get; set; }
	[Export] public int MaxMana { get; set; }
	[Export] public int Mana { get; set; }
	[Export] public int PhysicalArmor;

	[Signal] public delegate void PlayerHealthUpdatedEventHandler(int healthValue);

	// Called when the node enters the scene tree for the first time.
	public PlayerData() : this(0, 0, 0, 0, 0) {}

	public PlayerData(int maxHealth, int health, int maxMana, int mana, int physArmor)	
	{
		MaxHealth = maxHealth;
		Health = health;
		MaxMana = maxMana;
		Mana = mana;
		PhysicalArmor = physArmor;

	}
}
