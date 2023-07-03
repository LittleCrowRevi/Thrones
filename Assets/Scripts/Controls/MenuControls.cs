using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class MenuControls : MonoBehaviour
{

    public UnityEvent OpeningMenu;
    public UnityEvent ClosingMenu;

    private void Awake() 
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // TODO: checks for which menun can be openen...if it can be opened etc?
    void OnOpenMenu()
    {
        OpeningMenu.Invoke();
        gameObject.GetComponent<PlayerInput>().SwitchCurrentActionMap("menu");
        
    }

    void OnClose() 
    {
        ClosingMenu.Invoke();
        gameObject.GetComponent<PlayerInput>().SwitchCurrentActionMap("gameplay");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
