using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class MenuControls : MonoBehaviour
{

    GameObject player;

    private void Awake() 
    {
        BindPlayerEventsToUI();

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    public void OnOpeningMenu()
    {
        Debug.Log("UI LOG");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void BindPlayerEventsToUI()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        player.GetComponent<Player>().OpeningMenu.AddListener(OnOpeningMenu);
    }
}
