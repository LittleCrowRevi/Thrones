using System;
using System.Collections;
using System.Collections.Generic;
using Thrones.Scripts.UI;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public UnityEvent<float, String> LoadingProgress;
    public UnityEvent<String> NotifySceneChange;
    public UnityEvent<InterfaceID> OpeningMenu;
    public UnityEvent ClosingMenu;

    public enum GameState {
        Loading,
        InDungeon,
        InBattle,
        InMenu,
    }
    public bool teleporterUsable = false;

    // Instead save it as a custom Scene class which has position etc too?
    // then raises an event which triggers loading, hiding of ui and such perhaps?
    // OR: store scene and location seperately? for more convenient logic of in-scene and between sceenes teleporting
    private GameObject _currentLocation;
    GameObject[] previousLocations = new GameObject[5];
    // stroing a reference to the scene doesnt quite work...
    String _activeScene;
    public String activeScenePath
    {
        get { return _activeScene; }
        set { _activeScene = value; }
    }

    public String previousScenePath;
    public GameObject LoadingScreen;
    public GameObject MainMenu;

    private void Awake() 
    {
    }
    // Start is called before the first frame update
    void Start()
    {
        NotifySceneChange.Invoke("Dungeon");
        OpeningMenu.Invoke(InterfaceID.MainMenu);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            NotifySceneChange.Invoke("Dungeon-E2");
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            NotifySceneChange.Invoke("Dungeon");
        }
    }

    public void OnSceneChange(String newScenePath)
    {
        LoadingScreen.SetActive(true);
       
        Debug.Log("[Loading] Changing Scene from [" + activeScenePath + "] to [" +  newScenePath + "]");
        StartCoroutine(AsyncLoadNewScene(newScenePath));
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
                previousScenePath = activeScenePath;
                activeScenePath = newScenePath;

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

    IEnumerator AsyncUnloadScene(String scene) 
    {
        AsyncOperation asyncUnoad = SceneManager.UnloadSceneAsync(scene);
        while (!asyncUnoad.isDone)
        {
            yield return null;
        }
    }
}
