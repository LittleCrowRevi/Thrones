using Assets.Scripts;
using Assets.Scripts.States;
using System;
using System.Collections;
using System.Collections.Generic;
using Thrones.Scripts.UI;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Game: MonoBehaviour
{

    /// State Enum
    public enum State
    {
        Loading,
        Menu,
        Exploration,
        Combat,
    }

    /// Unity Events

    [field: SerializeField] public UnityEvent<float, string> LoadingProgress { get; set; }
    [field: SerializeField] public UnityEvent<string, State> NotifyLoadScene { get; set; }
    [field: SerializeField] public UnityEvent<InterfaceID> OnOpeningMenu { get; set; }
    [field: SerializeField] public UnityEvent OnClosingMenu { get; set; }

    /// Data fields

    public string ActiveScenePath { get; set; }
    public string previousScenePath { get; set; }

    /// Game Objects

    [field: SerializeField] public GameObject LoadingScreen { get; set; }

    /// State Stack

    public LoadingState LoadingState { get; set; }
    public ExplorationState ExplorationState { get; set; }
    public StateManager StateManager { get; set; }

    /// Methods

    // Gameloop methods
    private void Awake()
    {
        StateManager = new();
        LoadingState = new(StateManager);
        ExplorationState = new(StateManager);
    }

    void Start()
    {
        InitGame();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            Debug.Log("U: " + StateManager.CurrentState.Name);
            NotifyLoadScene.Invoke("EX.Dungeon", State.Exploration);
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            Debug.Log(StateManager.CurrentState.Name);
        }

        StateManager.Update();
    }

    // regular methods
    void InitGame()
    {
        LoadingState.NewScenePath = "EX.Dungeon";
        LoadingState.NextState = ExplorationState;
        LoadingScreen.SetActive(true);
        StateManager.InitialState(LoadingState);
        OnOpeningMenu.Invoke(InterfaceID.MainMenu);
    }

    public void OnSceneLoad(string newScenePath, State nextStateType)
    {
        LoadingState.NewScenePath = newScenePath;
        LoadingState.NextState = nextStateType switch
        {
            State.Exploration => ExplorationState,
            State.Loading => LoadingState,
            _ => throw new NotImplementedException(),
        };
        LoadingScreen.SetActive(true);
        Debug.Log(LoadingState.NewScenePath);
        StateManager.OnTransition(true, LoadingState);
    }

    IEnumerator AsyncLoadNewScene(string newScenePath)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(newScenePath, LoadSceneMode.Additive);
        asyncLoad.allowSceneActivation = false;
        while (!asyncLoad.isDone)
        {
            LoadingProgress.Invoke(asyncLoad.progress / 0.9F * 100F, newScenePath);

            if (asyncLoad.progress >= 0.9f)
            {
                asyncLoad.allowSceneActivation = true;
                previousScenePath = ActiveScenePath;
                ActiveScenePath = newScenePath;

                if (previousScenePath != null)
                {
                    Debug.Log("[Loading] previous scene: " + previousScenePath);
                    StartCoroutine(AsyncUnloadScene(previousScenePath));
                }
                LoadingScreen.SetActive(false);
            }
            yield return new WaitForSeconds(1F);
        }
    }

    IEnumerator AsyncUnloadScene(string scene) 
    {
        AsyncOperation asyncUnoad = SceneManager.UnloadSceneAsync(scene);
        while (!asyncUnoad.isDone)
        {
            yield return null;
        }
    }
}
