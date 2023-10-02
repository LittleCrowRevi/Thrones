using Godot;
using System;

public partial class HPBar : TextureProgressBar
{
	private Tween _tween;
	private Label HpLabel { get; set; } = new();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		AddChild(HpLabel);
		HpLabel.Scale = new Vector2(0.2F, 0.2F);
		HpLabel.Position = new Vector2(20F, 0F);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	// updates current and maxHealth
	public void UpdateHpInfo(int health, int maxHealth)
	{
		// change the HP Number display
		HpLabel.Text = $"{health.ToString()}/{maxHealth.ToString()}";
		// kill the previous tween to be sure an animation doesn't fail
		_tween?.Kill();
		_tween = CreateTween();
		// calculate the hp%
		var hpPercentage = health * 100 / maxHealth;
		// change and animate hp bar
		_tween.TweenProperty(this, "value", hpPercentage, 0.4).SetTrans(Tween.TransitionType.Bounce).SetEase(Tween.EaseType.Out);
	}

}
