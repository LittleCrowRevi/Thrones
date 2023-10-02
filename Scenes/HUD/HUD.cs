using Godot;
using Thrones;
using Thrones.Scripts;

namespace ThronesEra.Scenes.HUD;

public partial class HUD : CanvasLayer
{
	public HUD()
	{
		Name = "HUD";
		
		_stateLabel = new Label();
		_stateLabel.Position = new Vector2(980F, 30F);
		_stateLabel.Text = "state";
		_stateLabel.Visible = true;
		AddChild(_stateLabel);
		
		LoadingBar = new ProgressBar();
		LoadingBar.Size = new Vector2(320F, 50F);
		LoadingBar.Position = new Vector2(400F, 500F);
		AddChild(LoadingBar);
		
	}
	
	[Export] public StateManager StateManager { get; set; }
	private Label _stateLabel;
	public ProgressBar LoadingBar { get; set; }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (StateManager is not null)
		{
			_stateLabel.Text = StateManager.CurrentState?.Name;
		}
	}
}