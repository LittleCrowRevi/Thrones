using System.Threading.Tasks;
using Godot;
using Godot.Collections;
using ThronesEra;

namespace Thrones.Util;

public partial class GlobalLoader : Node
{
    public GlobalLoader()
    {
        Name = "GlobalLoader";
        InitLoadScene += LoadScene;
    }
    
    /// Signals
    [Signal] public delegate void InitLoadSceneEventHandler(string sceneName, bool unloadScene);

    /// Data
    public Node ActiveScene { get; set; }
    public ProgressBar LoadingBar { get; set; }

    public static Texture2D LoadTexture(string path)
    {
        var resource = GD.Load(path);
        return (Texture2D)resource;
    }

    /// <summary>
    ///     Async loads a packed scene entity
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public Resource LoadResource(string path)
    {
        ResourceLoader.LoadThreadedRequest(path);
        LoadingBar.Visible = true;
        var progress = new Array();
        while (ResourceLoader.LoadThreadedGetStatus(path, progress) != ResourceLoader.ThreadLoadStatus.Loaded)
        {
            LoadingBar.Value = (double)progress[0] * 100;
        }
        LoadingBar.Value = 0;
        LoadingBar.Visible = false;
        return ResourceLoader.LoadThreadedGet(path);
    }

    private void LoadScene(string path, bool unloadPrevious)
    {
        ResourceLoader.LoadThreadedRequest(path, cacheMode: ResourceLoader.CacheMode.Ignore);
        
        LoadingBar.Visible = true;
        var progress = new Array();
        while (ResourceLoader.LoadThreadedGetStatus(path, progress) != ResourceLoader.ThreadLoadStatus.Loaded)
        {
            LoadingBar.Value = (double)progress[0] * 100;
        }

        var nextScene = (PackedScene)ResourceLoader.LoadThreadedGet(path);

        if (unloadPrevious) ActiveScene?.Free();
        ActiveScene = nextScene.Instantiate();

        GetTree().Root.GetNode("GameManager/World").AddChild(ActiveScene);

        Logger.INFO($"Loaded new scene: {ActiveScene.Name}");
        LoadingBar.Value = 0;
        LoadingBar.Visible = false;
    }
    
}