using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLabel : MonoBehaviour
{
    private Text text;
    private SceneController sceneController;

    // Start is called before the first frame update
    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoad;
        SceneManager.sceneUnloaded += OnSceneUnload;
        text = GetComponent<Text>();
        GameObject gameManager = GameObject.FindGameObjectWithTag("GameController");
        sceneController = gameManager.GetComponent<SceneController>();
    }

    private void OnSceneUnload(Scene arg0)
    {
        text.text = "Level " + sceneController.CurrentLevel;
    }

    private void OnSceneLoad(Scene arg0, LoadSceneMode arg1)
    {
        text.text = "Level " + sceneController.CurrentLevel;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
