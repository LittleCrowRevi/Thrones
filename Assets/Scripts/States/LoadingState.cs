using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using static Thrones.Utilities.Util;
using System.Threading.Tasks;
using System.Threading;

namespace Assets.Scripts.States
{
    public class LoadingState : IState
    {
        // Unity Events

        // Data Fields
        readonly string _Name = "LoaadingState";
        public string Name { get { return _Name; } }
        public string ActiveScenePath { get; set; }
        public string NewScenePath { get; set; }
        public bool LoadingComplete { get; set; }
        public AsyncOperation AsyncSceneLoad { get; set; }
        public AsyncOperation AsyncSceneUnload { get; set; }
        public IState NextState { get; set; }
        public StateManager StateManager { get; set; }

        // GameObjects
        [field: SerializeField] public GameObject LoadingScreen { get; set; }

        public LoadingState(StateManager stateManager)
        {
            StateManager = stateManager;
        }

        public void Enter()
        {
            LoadingComplete = false;
            Debug.Log("[LoadingState] Entering LoadingState");
            AsyncSceneLoad = SceneManager.LoadSceneAsync(NewScenePath, LoadSceneMode.Additive);
            AsyncSceneLoad.allowSceneActivation = false;
        }

        public void Exit()
        {
            Debug.Log("[LoadingState] Exiting LoadingState");
        }

        public void Execute()
        {
            // Perhaps just checking if AsyncSceneLoad.isDone is enough?
            if (!LoadingComplete)
            {
                Debug.Log("[LoadingState] Progress Load");
                ProgressLoad();
            }
            if (AsyncSceneUnload != null && AsyncSceneUnload.progress >= 0.9f)
            {
                Debug.Log("[LoadingState] Unloaded Previous Scene");

            }
        }

        public void ProgressLoad()
        {
            GameRef().LoadingProgress.Invoke(AsyncSceneLoad.progress / 0.9F * 100F, NewScenePath);

            if (AsyncSceneLoad.progress >= 0.9f)
            {
                LoadingComplete = true;
                AsyncSceneLoad.allowSceneActivation = true;
                if (ActiveScenePath != null)
                {
                    Debug.Log("[LoadingState] Unloading Previous Scene: " + ActiveScenePath);
                    AsyncSceneUnload = SceneManager.UnloadSceneAsync(ActiveScenePath);
                }
                ActiveScenePath = NewScenePath;
                Debug.Log("[LoadingState] Loaded scene " + NewScenePath);
                GameRef().LoadingProgress.Invoke(100F, NewScenePath);
                StateManager.OnTransition(true, NextState);
            }

        }

    }
}
