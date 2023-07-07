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
    
    public UnityEvent<InterfaceID> OpeningMenu;
    public UnityEvent ClosingMenu;

    private void Awake() 
    {
        CreateNewPlayer();    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // pretty pointless
    public void InvokeEvents(int id)
    {
        Debug.Log("Invoking Player Events");
        switch(id)
        {
            case 0:
                OnOpenMainMenu();
                break;
            
            case 1:
                OnClose();
                break;
            case 2:
                OnOpenTabMenu();
                break;
        }
    }

    // TODO: checks for which menun can be openen...if it can be opened etc?
    // Input Events
    public void CreateNewPlayer()
    {
        Level = 0;
        MaxHealthPoints = 100;
        AttackPoints = 10;
    }

    // TODO: checks for which menun can be openen...if it can be opened etc?
    // Input Events
    void OnOpenMainMenu()
    {
        Debug.Log("[010101] Opening Menu!");
        OpeningMenu.Invoke(InterfaceID.MainMenu);
        gameObject.GetComponent<PlayerInput>().SwitchCurrentActionMap("UI");
        Debug.Log(gameObject.GetComponent<PlayerInput>().currentActionMap.ToString());
    }

    void OnClose() 
    {
        Debug.Log("[010102] Closing Menu!");
        ClosingMenu.Invoke();
        gameObject.GetComponent<PlayerInput>().SwitchCurrentActionMap("Player");
        Debug.Log(gameObject.GetComponent<PlayerInput>().currentActionMap.ToString());
    }

        // TODO: checks for which menun can be openen...if it can be opened etc?
    // Input Events
    void OnOpenTabMenu()
    {
        Debug.Log("[010103] Opening Tab Menu!");
        OpeningMenu.Invoke(InterfaceID.TabMenu);
        gameObject.GetComponent<PlayerInput>().SwitchCurrentActionMap("UI");
        Debug.Log(gameObject.GetComponent<PlayerInput>().currentActionMap.ToString());
    }

}
