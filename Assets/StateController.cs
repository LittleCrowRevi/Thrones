using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour
{
    public enum GameState {
        Loading,
        InDungeon,
        InBattle,
        InMenu,
    }

    GameObject currentLocation;
    GameObject[] previousLocations = new GameObject[5];
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
