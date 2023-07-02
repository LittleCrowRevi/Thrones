using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public enum GameState {
        Loading,
        InDungeon,
        InBattle,
        InMenu,
    }

    // Instead save it as a custom Scene class which has position etc too?
    // then raises an event which triggers loading, hiding of ui and such perhaps?
    // OR: store scene and location seperately? for more convenient logic of in-scene and between sceenes teleporting
    private GameObject _currentLocation;
    GameObject currentLocation {
        set {
            _currentLocation = value;
        }
    }
    GameObject[] previousLocations = new GameObject[5];
    private GameObject _currentScene;
    public bool teleporterUsable = false;
    
    private void Awake() 
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
