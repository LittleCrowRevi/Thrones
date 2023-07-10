using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Thrones.Scripts.Utilities.Util;

public class LoadingScreen : MonoBehaviour
{
    public GameObject loadingText;

    private void OnEnable()
    {
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnLoadingProgressChange(float progress, string newScene)
    {
        if (loadingText != null)
        {
            string s = newScene + " | " + progress.ToString() + "%";
            Debug.Log("[Loading]: " + s);
            loadingText.GetComponent<TMP_Text>().text = s;
        }
    }
    
}
