using Godot;
using System;


namespace Thrones.Util
{
    public partial class SceneLoader : Node
    {
        
        // Signals

        [Signal] public delegate void InitLoadSceneEventHandler(string sceneName, bool unloadScene);

        // Data

        public Node ActiveScene { get; set; }
        

        // Methods 

        public override void _Ready()
        {
            Viewport root = GetTree().Root;
            ActiveScene = root.GetNode<Node>("GameManager").GetNode("World").GetChildOrNull<Node>(0);
            GD.Print($"[INFO] current scene: {ActiveScene?.Name}");

            InitLoadScene += GotoScene;
        }

        public void GotoScene(string path, bool unloadPrevious)
        {
            // This function will usually be called from a signal callback,
            // or some other function from the current scene.
            // Deleting the current scene at this point is
            // a bad idea, because it may still be executing code.
            // This will result in a crash or unexpected behavior.

            // The solution is to defer the load to a later time, when
            // we can be sure that no code from the current scene is running:
            CallDeferred(nameof(DeferredGotoScene), path, unloadPrevious);
        }

        public void DeferredGotoScene(string path, bool unloadPrevious)
        {
            // It is now safe to remove the current scene
            if (unloadPrevious) ActiveScene?.Free();

            var nextScene = (PackedScene)GD.Load(path);

            ActiveScene = nextScene.Instantiate();

            GetTree().Root.AddChild(ActiveScene);
            
            GetTree().CurrentScene = ActiveScene;

        }
    }
}
