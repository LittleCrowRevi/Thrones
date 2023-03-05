using Godot;
using System;

public partial class GlobalManager : Node
{

	public enum GameState {

		Loading, 
		InGame,
		MainMenu,
		Battle

	}
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ConnectHUD();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void ConnectHUD()
	{
		var hud = GetNode<HUD>("HUD");
		var playerHealthComponent = GetNode<Player>("Player").GetNode<HealthComponent>("HealthComponent");
		playerHealthComponent.Connect(HealthComponent.SignalName.HealthUpdated, new Callable(hud, HUD.MethodName._on_player_health_updated));
		playerHealthComponent.Update();
	}
}
