using Godot;
using System;

public partial class hud : CanvasLayer
{

	private Tween _tween;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// connect the health change signal
		var player = GetNode<Player>("/root/TestScreen/Player");
		player.HealthChanged += _on_player_health_changed; 

		// load hp data
		var playerData = (PlayerData)GetNode("/root/PlayerData");
		var hpNumbers = (Label)GetNode("Label");
		hpNumbers.Text = $"{playerData.currentHealth.ToString()}/{playerData.health.ToString()}";
		var hpProgressBar = (TextureProgressBar)GetNode("HP-Bar");	
		_tween = CreateTween();
		_tween.TweenProperty(hpProgressBar, "value", playerData.currentHealth, 0.2).SetTrans(Tween.TransitionType.Bounce).SetEase(Tween.EaseType.Out);

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	// function which controls the hp progress bar
	public void _on_player_health_changed(int changeValue)
	{

		// change hp numbers
		var hpNumbers = (Label)GetNode("Label");
		var playerData = (PlayerData)GetNode("/root/PlayerData");
		hpNumbers.Text = $"{playerData.currentHealth.ToString()}/{playerData.health.ToString()}";
		
		// kill the previous tween to be sure an animation doesn't fail
		if (_tween != null)
			_tween.Kill();

		// change animate the hp bar
		var hpProgressBar = (TextureProgressBar)GetNode("HP-Bar");	
		_tween = CreateTween();
		_tween.TweenProperty(hpProgressBar, "value", hpProgressBar.Value + changeValue, 0.2).SetTrans(Tween.TransitionType.Bounce).SetEase(Tween.EaseType.Out);

		GD.Print($"[INFO] HP changed by {changeValue}");
	}
}
