using System;
using System.Collections;
using System.Collections.Generic;
using Thrones.Scripts.UI;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class TabView : MonoBehaviour
{
    VisualElement _container;

    private void OnEnable() 
    {
        _container = gameObject.GetComponent<UIDocument>().rootVisualElement;
    }

    public void OpenTabMenu(InterfaceID id)
    {
        // why is this called twice when not done through Unity Interface?
        if (_container != null && id == InterfaceID.TabMenu)
        {
            Debug.Log("Opening Tab Menu");
            _container.RemoveFromClassList("hidden");
        }
    }

    public void HideTabMenu()
    {
        if (_container != null && !_container.ClassListContains("hidden"))
        {
            Debug.Log("Hiding Tab Menu");
            _container.AddToClassList("hidden");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        HideTabMenu();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
