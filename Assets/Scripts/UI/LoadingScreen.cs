using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Thrones.UIControl
{
    public class LoadingScreen : MonoBehaviour
    {
        public GameObject LoadingText { get; set; }

        public void OnLoadingProgressChange(float progress, string newScene)
        {
            if (LoadingText != null)
            {
                string s = newScene + " | " + progress.ToString() + "%";
                Debug.Log("[Loading]: " + s);
                LoadingText.GetComponent<TMP_Text>().text = s;
            }
            if (progress == 100f)
            {
                gameObject.SetActive(false);
            }
        }
    
    }
}
