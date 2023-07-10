using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using UnityEngine.UIElements;
using Thrones.Scripts.UI;
using Unity.VisualScripting;
using UnityEngine.EventSystems;
using static Thrones.Scripts.Utilities.Util;

//TODO: https://forum.unity.com/threads/global-pointer-events.283895/
//      System to handle selecting UI better
public class UIController : MonoBehaviour
{

    GameObject player;
    GameObject mainMenu;

    GameObject _activeMenu;
    public GameObject ActiveMenu {
        set {
            _activeMenu?.SetActive(false);
            _activeMenu = value;
            if (!_activeMenu.activeSelf)
                {
                    _activeMenu.SetActive(true);
                }
            }
            get {
            return _activeMenu;
        }
    }

    void OnEnable()
    {
        mainMenu = FindChildObject(gameObject, "MainMenu");
        _activeMenu = mainMenu;
        BindPlayerEventsToUI();

    }

    // not needed since doing it through unity is better?
    void BindPlayerEventsToUI()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

    }

    public void OpenMenu(InterfaceID id)
    {
        switch (id)
        {
            case InterfaceID.MainMenu:
                ActiveMenu = mainMenu;
                Debug.Log("[010101] Opening Menu!");
                break;
            case InterfaceID.TabMenu:
                ActiveMenu = mainMenu;
                Debug.Log("[010103] Opening Tab Menu!");
                break;
        }
        player.GetComponent<PlayerInput>().SwitchCurrentActionMap("UI");
        Debug.Log("[0201] Set Player Input to: " + player.GetComponent<PlayerInput>().currentActionMap.ToString());
    }

    public void CloseActiveMenu() 
    {
        Debug.Log("[010102] Closing Menu!");
        ActiveMenu.SetActive(false);
        player.GetComponent<PlayerInput>().SwitchCurrentActionMap("Player");
        Debug.Log("[0201] Set Player Input to: " + player.GetComponent<PlayerInput>().currentActionMap.ToString());
    }

}