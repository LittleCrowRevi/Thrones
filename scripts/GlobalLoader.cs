using Godot;
using System;

public partial class GlobalLoader : Node
{

	
	public Node currentScene { get; set; }
	public Node battleScene;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Viewport root = GetTree().Root;
		currentScene = root.GetNode<Node>("GlobalManager").GetChild(0).GetChild(0);
		GD.Print($"[INFO] current scene: {currentScene.Name}");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}

	public void GotoScene(string path, bool battle = false)
	{
		// This function will usually be called from a signal callback,
		// or some other function from the current scene.
		// Deleting the current scene at this point is
		// a bad idea, because it may still be executing code.
		// This will result in a crash or unexpected behavior.

		// The solution is to defer the load to a later time, when
		// we can be sure that no code from the current scene is running:
		CallDeferred(nameof(DeferredGotoScene), path, battle);
	}

	public void DeferredGotoScene(string path, bool battle)
  {
		// It is now safe to remove the current scene
		if (!battle)
		{
		currentScene.Free();

		// Load a new scene.
		var nextScene = (PackedScene)GD.Load(path);

		// Instance the new scene.
		currentScene = nextScene.Instantiate();

		// Add it to the active scene, as child of root.
		GetTree().Root.AddChild(currentScene);

		// Optionally, to make it compatible with the SceneTree.change_scene_to_file() API.
		GetTree().CurrentScene = currentScene;

		return;

		} else if (battle) {

			var newBattleScene = (PackedScene)GD.Load(path);

			battleScene = newBattleScene.Instantiate();

			GetTree().Root.GetNode<Node>("GlobalManager").GetChild(0).AddChild(battleScene);
		}
	}
}
