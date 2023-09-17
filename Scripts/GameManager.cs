using Godot;
using System;
using Thrones.Scripts;
using Thrones.Util;

namespace Thrones
{
	public partial class GameManager : Node2D
	{
		/// nodes

		[Export] public Camera2D Camera { get; set; }
		[Export] public Node World { get; set; }
		[Export] public Node sceneLoader { get; set; }

		/// signals

		[Signal] public delegate void ChangeControlledPCEventHandler(Node target);

		/// states

		[Export] public StateManager StateManager { get; set; }

		/// methods 

		public override void _Ready()
		{
			InitGame();
		}

		public override void _Process(double delta)
		{
		}

		void InitGame()
		{
			sceneLoader.EmitSignal(SceneLoader.SignalName.InitLoadScene, "res://Scenes/Levels/dev_level.tscn", true);
			ChangeControlledPC += Camera.GetScript().As<GlobalCamera>().OnChangeTarget;
		}

		public static GameManager GetGameScript(Node node)
		{
			return node.GetTree().Root.GetNode("GameManager").GetScript().As<GameManager>();
		}
	}

}
