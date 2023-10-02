using Godot;
using Thrones;
using Thrones.Scripts;

namespace ThronesEra.Scenes.HUD;

public partial class HUD : CanvasLayer
{
	[Export] public StateManager StateManager { get; set; }
	private Label _stateLabel;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_stateLabel = new Label();
		_stateLabel.Position = new Vector2(980F, 30F);
		_stateLabel.Text = "state";
		_stateLabel.Visible = true;
		AddChild(_stateLabel);
		StateManager = (StateManager)GetNode("/root/GameManager/StateManager");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (StateManager is not null)
		{
			_stateLabel.Text = StateManager.CurrentState.Name;
		}
	}
}