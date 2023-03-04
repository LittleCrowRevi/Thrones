using Godot;
using System;

// TODO: Update signal and function which updates the hud upon receiving the signal
// maybe make it a global singleton handling all UIs?
public partial class HUD : CanvasLayer
{

	private Tween _tween;
	private Label HpLabel;
	private TextureProgressBar HpProgressBar;

	// TODO: Give all UI Nodes a script handling all their respective logic??
	[Signal] public delegate void UpdatedHPEventHandler();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

		// load labels
		HpLabel = (Label)GetNode("Label");
		HpProgressBar = (TextureProgressBar)GetNode("HP-Bar");

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}

	// function which controls the hp progress bar
	public void _on_player_health_updated(int previousHealth, int health, int MaxHealth)
	{
		UpdatePlayerHPInfo(health, MaxHealth);
	}

	// updates current and maxHealth
	private void UpdatePlayerHPInfo(int health, int maxHealth)
	{
		// change the HP Number display
		HpLabel.Text = $"{health.ToString()}/{maxHealth.ToString()}";
		// kill the previous tween to be sure an animation doesn't fail
		if (_tween != null)
			_tween.Kill();
		_tween = CreateTween();
		// calculate the hp%
		var hpPercentage = health * 100 / maxHealth;
		// change and animate hp bar
		_tween.TweenProperty(HpProgressBar, "value", hpPercentage, 0.2).SetTrans(Tween.TransitionType.Bounce).SetEase(Tween.EaseType.Out);
	}

}
