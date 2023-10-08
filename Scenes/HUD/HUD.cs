using Godot;
using Thrones;
using Thrones.Scripts;

namespace ThronesEra.Scenes.HUD;

public partial class HUD : CanvasLayer
{
	public HUD()
	{
		Name = "HUD";

		_stateLabel = new Label
		{
			Name = "StateLabel",
			Position = new Vector2(980F, 30F),
			Text = "state",
			Visible = true
		};
		AddChild(_stateLabel);

		LoadingBar = new ProgressBar
		{
			Name = "LoadingBar",
			Size = new Vector2(320F, 50F),
			Position = new Vector2(400F, 500F)
		};
		AddChild(LoadingBar);

		_manaLabel = new Label
		{
			Name = "ManaLabel",
			Position = new Vector2(20F, 50F),
			Text = "mana",
			Visible = true
		};
		AddChild(_manaLabel);

		SkillList = new ItemList
		{
			Name = "SkillList",
			Position = new Vector2(900F, 300F),
			Size = new Vector2(150F, 200f)
		};
		AddChild(SkillList);

	}
	
	[Export] public StateManager StateManager { get; set; }
	private Label _stateLabel;
	private Label _manaLabel;
	public ProgressBar LoadingBar { get; set; }
	public ItemList SkillList { get; set; }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (StateManager is not null)
		{
			_stateLabel.Text = StateManager.CurrentState?.Name;
		}
	}

	public void UpdateMana(int currentMana, int totalMana)
	{
		var mana = $"{currentMana}/{totalMana}";
		_manaLabel.Text = mana;
	}
}