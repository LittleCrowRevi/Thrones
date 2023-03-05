using Godot;
using System;

public partial class Player : CharacterBody2D
{

	[Export] public float Speed = 300.0f;
	[Export] public PackedScene floatingText = (PackedScene)ResourceLoader.Load("res://scenes/FloatingText.tscn");
	[Export] public Resource data;

	public Godot.AnimationTree AnimTree;

	// Variable containing min and max Zoom Levels
	private Vector2 _ZoomLevels = new Vector2(2.5f, 5.0f);

	public override void _PhysicsProcess(double delta)
	{
		// calls the input function to handle walk anim and velocity, zoom etc
		GetInput(delta);
		MoveAndSlide();
	}

	// called when the Player enters the scene
	public override void _Ready() 
	{

		AnimTree = GetNode<Godot.AnimationTree>("AnimationTree");
		AnimTree.Set("parameters/conditions/idle", (Velocity == new Vector2(0, 0)));

		// load data to components
		ConnectChildrenSignals();

	}

	public void ConnectChildrenSignals()
	{
		var HealthComponent = (HealthComponent)GetNode("HealthComponent");
		
		//GetNode<GlobalEvents>("/root/GlobalEvents").Connect(GlobalEvents.SignalName.PlayerHealthUpdated, new Callable(HealthComponent, HealthComponent.MethodName._health_change));
	}

	
	// TODO: OnDamage and OnHeal maybe refactor into a single EventHandler? 
	// Make damage and such be handeld by the root scene? So that it can be independent from any entities
	private void OnDamage(int damage)
	{

		var calculatedDamage = damage;
		
		// floating numbers
		var floatingDamage = (FloatingText)floatingText.Instantiate();
		floatingDamage.Amount = calculatedDamage;
		floatingDamage.type = FloatingText.TextType.Damage;
		AddChild(floatingDamage);

		GetNode<HealthComponent>("HealthComponent").EmitSignal(HealthComponent.SignalName.HealthChange, calculatedDamage);

	}
	
	private void OnHeal(int heal)
	{
		
		// floating numbers
		var floatingHeal = (FloatingText)floatingText.Instantiate();
		floatingHeal.Amount = heal;
		floatingHeal.type = FloatingText.TextType.Heal;
		AddChild(floatingHeal);

		GetNode<HealthComponent>("HealthComponent").EmitSignal(HealthComponent.SignalName.HealthChange, heal);
	}
	
	private void GetInput(double delta)
	{
		var sprite = (Sprite2D)GetNode("Sprite2D");
		AnimationNodeStateMachinePlayback stateMachine = (AnimationNodeStateMachinePlayback)AnimTree.Get("parameters/playback");

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
		
		Vector2 velocity = Velocity;
		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		// Changes the walking animation based on velocity input and
		// calculates velocity
		if (direction != Vector2.Zero)
		{
			
			// TODO: change the sprite itself rather than flip it here
			if (direction == Vector2.Right)
			{
				sprite.FlipH = false;
			} else if (direction == Vector2.Left) {
				sprite.FlipH = true;
			}
			// Calculates the walking velocity
			velocity = direction.Normalized() * Speed;

			// set the walking animation
			AnimTree.Set("parameters/Walk/blend_position", velocity);
			stateMachine.Travel("Walk");


		// if there is no walk input switches to idle
		} else {
			velocity = Vector2.Zero;
			stateMachine.Travel("Idle");

		}
		
		Velocity = velocity;
		
	}
}
