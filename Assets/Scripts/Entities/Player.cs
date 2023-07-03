using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    
    public UnityEvent OpeningMenu;
    public UnityEvent ClosingMenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

        // TODO: checks for which menun can be openen...if it can be opened etc?
    void OnOpenMenu()
    {
        Debug.Log("[010101] Opening Menu!");
        OpeningMenu.Invoke();
        gameObject.GetComponent<PlayerInput>().SwitchCurrentActionMap("UI");
        Debug.Log(gameObject.GetComponent<PlayerInput>().currentActionMap.ToString());
    }

    void OnCloseMenu() 
    {
        Debug.Log("[010102] Closing Menu!");
        ClosingMenu.Invoke();
        gameObject.GetComponent<PlayerInput>().SwitchCurrentActionMap("Player");
        Debug.Log(gameObject.GetComponent<PlayerInput>().currentActionMap.ToString());
    }
}
