using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Thrones.Utilities {
    public static class Util
    {
        /// <summary>
        /// Static convenience method for finding a child object of a gameobject
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="name">Name of the child object</param>
        /// <returns>child object of the inserted parent object</returns>
        public static GameObject FindChildObject(GameObject parent, string name)
        {
            Transform[] childTransforms = parent.GetComponentsInChildren<Transform>(true);
            foreach(Transform child in childTransforms)
            {
                if(child.name == name) return child.gameObject;
            }
            return null;
        }

        /// <summary>
        /// Static method for a reference to the Game Object
        /// </summary>
        /// <returns>Game object Reference</returns>
        public static Game GameRef()
        {
            return GameObject.Find("Game").GetComponent<Game>();
        }

    }

}