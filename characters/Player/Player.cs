using Godot;
using System;

public partial class Player : CharacterBody2D
{

	[Export]
	public float Speed = 300.0f;
	public PackedScene floatingText;

	[Signal]
	public delegate void HealthChangedEventHandler(int healthValue);

	// Variable containing min and max Zoom Levels
	private Vector2 _ZoomLevels = new Vector2(2.5f, 5.0f);


	public override void _PhysicsProcess(double delta)
	{
		// calls the input function to handle walk anim and velocity, zoom etc
		GetInput(delta);
		MoveAndSlide();
	}

	// called when the Player enters the scene
	public override void _Ready() {

		// sets the animation to idle once the Player loads in
		// not really needed but eh, can't hurt to be safe
		var animatedSprite = (AnimatedSprite2D)GetNode("AnimatedSprite2D");
		animatedSprite.Animation = "idle";
		animatedSprite.Play();

		// floating text preload
		floatingText = (PackedScene)ResourceLoader.Load("res://scenes/FloatingText.tscn");

	}

	// TODO: OnDamage and OnHeal maybe refactor into a single EventHandler? 
	// Make damage and such be handeld by the root scene? So that it can be independent from any entities
	private void OnDamage(int damage)
	{
		var playerData = (PlayerData)GetNode("/root/PlayerData");
		var currentHealth = playerData.currentHealth;
		currentHealth += damage;
		playerData.currentHealth = playerData.currentHealth <= 0 ? 0 : currentHealth;

		
		
		// floating numbers
		var floatingDamage = (FloatingText)floatingText.Instantiate();
		floatingDamage.Amount = damage;
		floatingDamage.type = FloatingText.TextType.Damage;
		AddChild(floatingDamage);

		var globalEvents = (GlobalEvents)GetNode("/root/GlobalEvents");
		globalEvents.EmitSignal(GlobalEvents.SignalName.PlayerHealthChanged, damage);
	}
	
	private void OnHeal(int heal)
	{
		var playerData = (PlayerData)GetNode("/root/PlayerData");
		var currentHealth = playerData.currentHealth;
		currentHealth +=  heal;
		playerData.currentHealth = currentHealth >= playerData.health ? playerData.health : currentHealth;

		// floating numbers
		var floatingHeal = (FloatingText)floatingText.Instantiate();
		floatingHeal.Amount = heal;
		floatingHeal.type = FloatingText.TextType.Heal;
		AddChild(floatingHeal);

		var globalEvents = (GlobalEvents)GetNode("/root/GlobalEvents");
		globalEvents.EmitSignal(GlobalEvents.SignalName.PlayerHealthChanged, heal);
	}

	private void GetInput(double delta)
	{
		Vector2 velocity = Velocity;
		var animatedSprite = (AnimatedSprite2D)GetNode("AnimatedSprite2D");
		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");

		// test damage function
		// TODO: Maybe turn the method calls into signals?
		if (Input.IsActionJustReleased("damage_button"))
		{
			OnDamage(-20);
		}
		if (Input.IsActionJustReleased("heal_button"))
		{
			OnHeal(+20);
		}

		// Zoom Action
		var camera2D = (Camera2D)GetNode("Camera2D");
		if (Input.IsActionJustReleased("scroll_up"))
		{
			Vector2 currentZoom = camera2D.Zoom;
			float clampedZoom = Math.Clamp(currentZoom.X * 1.1f, _ZoomLevels.X, _ZoomLevels.Y);
			camera2D.Zoom = new Vector2(clampedZoom, clampedZoom);
		}
		if (Input.IsActionJustReleased("scroll_down"))
		{
			Vector2 currentZoom = camera2D.Zoom;
			float clampedZoom = Math.Clamp(currentZoom.X * 0.8f, _ZoomLevels.X, _ZoomLevels.Y);
			camera2D.Zoom = new Vector2(clampedZoom, clampedZoom);
		}
		
		// Changes the walking animation based on velocity input and
		// calculates velocity
		if (direction != Vector2.Zero)
		{
			if (direction == Vector2.Up)
			{
				animatedSprite.Animation = "walk-up";
			} else if (direction == Vector2.Right)
			{
				animatedSprite.Animation = "walk-horizontal";
				animatedSprite.FlipH = false;
			} else if (direction == Vector2.Left)
			{
				animatedSprite.Animation = "walk-horizontal";
				animatedSprite.FlipH = true;
			} else if (direction == Vector2.Down)
			{
				animatedSprite.Animation = "walk-down";
			}
			// Calculates the walking velocity
			velocity = direction.Normalized() * Speed;

		// if there is no walk input switches to idle
		} else
		{
			animatedSprite.Animation = "idle";
			velocity = Vector2.Zero;
		}
		animatedSprite.Play();
		Velocity = velocity;
		
	}
}
