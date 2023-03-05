using Godot;
using System;

public partial class HealthComponent : Node
{

	[Export] public int MaxHealth;
	[Export] public int Health;

	// informs all listeners to the health update
	[Signal] public delegate void HealthUpdatedEventHandler(int previousHealth, int currentHealth, int MaxHealth);
	// handles damage, health etc
	[Signal] public delegate void HealthChangeEventHandler(int changeValue);
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Connect(SignalName.HealthChange, new Callable(this, MethodName._health_change));
	}

	public void Update()
	{
		EmitSignal(SignalName.HealthUpdated, 0, Health, MaxHealth);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	// adds damage, heal etc to the current Health and checks if over Max or below 0 TODO: death check
	private void _health_change(int changeValue)
	{
		var previousHealth = Health;
		var newHealth = Health + changeValue;
		Health = newHealth > MaxHealth ? MaxHealth : newHealth < 0 ? 0 : newHealth;
		GD.Print($"[INFO] Health changed to {Health}");
		EmitSignal(SignalName.HealthUpdated, previousHealth, Health, MaxHealth);
	}
}
 