using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimerLabel : MonoBehaviour
{
    private Text text;

    // Start is called before the first frame update
    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoad;
        SceneManager.sceneUnloaded += OnSceneUnload;
        text = GetComponent<Text>();
    }

    private void OnSceneUnload(Scene arg0)
    {
        text.text = "Get Ready";
    }

    private void OnSceneLoad(Scene arg0, LoadSceneMode arg1)
    {
        text.text = "Time Left";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
