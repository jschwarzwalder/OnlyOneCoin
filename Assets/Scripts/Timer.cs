using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private SceneController sceneController;
    private Text text;

    // Start is called before the first frame update
    void Start()
    {
        GameObject gameManager = GameObject.FindGameObjectWithTag("GameController");
        sceneController = gameManager.GetComponent<SceneController>();
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        float timeLeft = sceneController.TimeLeft;
        int minutes = ((int)timeLeft) / 60;
        float seconds = timeLeft - minutes * 60;

        text.text = string.Format("{0}:{1}", minutes, seconds.ToString("00.0"));
    }
}
