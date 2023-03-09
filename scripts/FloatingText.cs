using Godot;
using System;

// TODO: Fix text color change to all floatingNumbers once a new one is spawned
// Make this into a global manager?
public partial class FloatingText : Marker2D	
{

	[Export]
	public int Amount;
	[Export]
	public TextType type;
	[Export]
	public Vector2 velocity;

	// enum for Textstyle depending on floatinValue type
	public enum TextType {
		Damage,
		Heal
	}



	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var label = (Label)GetNode("Label");
		label.Text = Amount.ToString();	

		// switch text theme depending on type
		switch(type)
		{
			case TextType.Damage:
				label.LabelSettings.FontColor = new Color("f56a63");
				break;
			case TextType.Heal:
				label.LabelSettings.FontColor = new Color("84f586");
				break;
			default:
				break;
		}

		var rng = new RandomNumberGenerator();
		rng.Randomize();
		var mov = rng.Randi() % 41;
		velocity = new Vector2(mov, 20);

		// text animation
		var tween = CreateTween();
		tween.TweenProperty(GetNode("Label"), "scale", new Vector2(1.1f, 1.1f), 0.2).SetTrans(Tween.TransitionType.Linear);
		tween.TweenProperty(GetNode("Label"), "scale", new Vector2(0.1f, 0.1f), 0.5).SetTrans(Tween.TransitionType.Linear);
		tween.Finished += _on_tween_finished;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Position += velocity * ((float)delta);
	}

	private void _on_tween_finished()
	{
		QueueFree();
	}

}
