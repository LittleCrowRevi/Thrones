using System.Threading.Tasks;
using Godot;
using Godot.Collections;
using ThronesEra;

namespace Thrones.Util;

public partial class GlobalLoader : Node
{
    /// Signals
    [Signal] public delegate void InitLoadSceneEventHandler(string sceneName, bool unloadScene);

    /// Data
    public Node ActiveScene { get; set; }

    /// Methods
    public GlobalLoader()
    {
        Name = "GlobalLoader";
        InitLoadScene += LoadScene;
    }

    /// Nodes 
    public ProgressBar loadingBar { get; set; }
    
    public override void _Ready()
    {
        Viewport root = GetTree().Root;
        ActiveScene = root.GetNode<Node>("GameManager").GetNode("World").GetChildOrNull<Node>(0);

    }

    public static Texture2D LoadTexture(string path)
    {
        var resource = GD.Load(path);
        return (Texture2D)resource;
    }

    /// <summary>
    /// Async loads a packed scene entity
    /// </summary>
    /// <param name="entityPath"></param>
    /// <returns></returns>
    public static async Task<Resource> LoadEntity(string entityPath)
    {
        ResourceLoader.LoadThreadedRequest(entityPath);

        while (ResourceLoader.LoadThreadedGetStatus(entityPath) != ResourceLoader.ThreadLoadStatus.Loaded)
            await Task.Delay(500);

        return ResourceLoader.LoadThreadedGet(entityPath);
    }

    private void LoadScene(string path, bool unloadPrevious)
    {
        ResourceLoader.LoadThreadedRequest(path, cacheMode: ResourceLoader.CacheMode.Ignore);
        loadingBar.Visible = true;
        var progress = new Array();
        while (ResourceLoader.LoadThreadedGetStatus(path, progress) != ResourceLoader.ThreadLoadStatus.Loaded)
        {
            Logger.INFO(progress);
            loadingBar.Value = (double)progress[0] * 100;
        }

        var nextScene = (PackedScene)ResourceLoader.LoadThreadedGet(path);
        
        if (unloadPrevious) ActiveScene?.Free();
        ActiveScene = nextScene.Instantiate();

        GetTree().Root.GetNode("GameManager/World").AddChild(ActiveScene);

        Logger.INFO($"Loaded new scene: {ActiveScene.Name}");
        loadingBar.Value = 0;
        loadingBar.Visible = false;
    }

    public void GotoScene(string path, bool unloadPrevious)
    {
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

        GetTree().Root.GetNode("GameManager/World").AddChild(ActiveScene);

        Logger.INFO($"Loaded new scene: {ActiveScene.Name}");
    }
}