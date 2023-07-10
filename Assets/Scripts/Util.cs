using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Thrones.Scripts.Utilities {
    public static class Util
    {
        public static GameObject FindChildObject(GameObject parent, string name)
        {
            Transform[] trs= parent.GetComponentsInChildren<Transform>(true);
            foreach(Transform t in trs)
            {
                if(t.name == name)
                {
                    return t.gameObject;
                }
            }
            return null;
        }

    }

}