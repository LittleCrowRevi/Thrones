using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using UnityEngine.UIElements;
using Thrones.Scripts.UI;

public class MainMenu : MonoBehaviour
{

    GameObject player;
    UIDocument ui;

    void OnEnable()
    {
        ui = gameObject.GetComponent<UIDocument>();
        BindPlayerEventsToUI();
    }

    public void OpenMainMenu(InterfaceID id)
    {
        // why is this called twice when not done through Unity Interface?
        if (ui != null && id == InterfaceID.MainMenu)
        {
            ui.rootVisualElement.RemoveFromClassList("hidden");
        }
    }

    public void HideMainMenu()
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
                player.GetComponent<Player>().InvokeEvents(1);
            };

        }
        //player.GetComponent<Player>().OpeningMenu.AddListener(OnOpeningMenu);
        //player.GetComponent<Player>().OpeningMenu.AddListener(OnClosingMenu);

    }
}
