using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using Thrones.Entities.Interfaces;
using Thrones.Scripts.UI;
using static Thrones.Utilities.Util;

namespace Thrones.Entities
{

    // TODO: Refactor controls to the GameControl Object?
    public class Player : IEntities
    {

        private void Awake() 
        {
            CreateNewPlayer();    
        }

        // TODO: checks for which menu can be openen...if it can be opened etc?
        // Input Events
        public void CreateNewPlayer()
        {
            Level = 0;
            MaxHealthPoints = 100;
            AttackPoints = 10;
        }

        void OnOpenMainMenu()
        {
            GameRef().OnOpeningMenu.Invoke(InterfaceID.MainMenu);
        }

        void OnClose()
        {
            GameRef().OnClosingMenu.Invoke();
        }

        void OnOpenTabMenu()
        {
            GameRef().OnOpeningMenu.Invoke(InterfaceID.TabMenu);
        }

    }
}
