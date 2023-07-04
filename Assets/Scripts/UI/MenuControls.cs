using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class MenuControls : MonoBehaviour
{

    GameObject player;
    UIDocument ui;

    void OnEnable()
    {
        ui = GetComponent<UIDocument>();
        BindPlayerEventsToUI();
    }

    public void OnOpeningMenu()
    {
        // why is this called twice when not done through Unity Interface?
        if (ui != null)
        {
            ui.rootVisualElement.RemoveFromClassList("hidden");
        }
    }

    public void OnClosingMenu()
    {
        if (!ui.rootVisualElement.ClassListContains("hidden"))
        {
            ui.rootVisualElement.AddToClassList("hidden");
        }
    }

    // not needed since doing it through unity is better?
    void BindPlayerEventsToUI()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        if (ui != null)
        {
            Button _button = ui.rootVisualElement.Q<Button>("resumeButton");
            _button.RegisterCallback<NavigationMoveEvent>(e =>
        {
            if (e.direction == NavigationMoveEvent.Direction.Down) return;
            e.PreventDefault();
        });
            _button.clicked += () => 
            {
                Debug.Log("resume button debug");
                player.GetComponent<Player>().InvokeEvents(1);
            };

        }
        //player.GetComponent<Player>().OpeningMenu.AddListener(OnOpeningMenu);
        //player.GetComponent<Player>().OpeningMenu.AddListener(OnClosingMenu);

    }
}
