using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using Thrones.Entities.Interfaces;
using Thrones.Scripts.UI;

// TODO: Refactor controls to the GameControl Object?
public class Player : IEntities
{

    private void Awake() 
    {
        CreateNewPlayer();    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // TODO: checks for which menun can be openen...if it can be opened etc?
    // Input Events
    public void CreateNewPlayer()
    {
        Level = 0;
        MaxHealthPoints = 100;
        AttackPoints = 10;
    }

    void OnOpenMainMenu()
    {
        GameObject.Find("Game").GetComponent<Game>().OpeningMenu.Invoke(InterfaceID.MainMenu);
    }

    void OnClose()
    {
        GameObject.Find("Game").GetComponent<Game>().ClosingMenu.Invoke();
    }

}
