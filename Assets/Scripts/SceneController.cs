using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public float TimeBetweenScenes = 15;
    public float TimeBeforeFirstScene = 5;
    public string[] Scenes;

    private int sceneIndex = 0;
    private bool sceneLoaded = false;
    private float nextTime;

    private AsyncOperation loadSceneOp;

    // Start is called before the first frame update
    void Start()
    {
        nextTime = Time.time + TimeBeforeFirstScene;
    }

    // Update is called once per frame
    void Update()
    {
        if (loadSceneOp != null)
        {
            if (loadSceneOp.isDone)
            {
                sceneLoaded = true;
                GameObject dataObject = GameObject.FindGameObjectWithTag("SceneData");
                if (dataObject)
                {
                    SceneData data = dataObject.GetComponent<SceneData>();
                    nextTime = Time.time + data.TimeLimit;
                }
                else
                {
                    nextTime = float.PositiveInfinity;
                }

                loadSceneOp = null;
            }

            return;
        }

        if (Time.time >= nextTime)
        {
            if (sceneLoaded)
            {
                Debug.Log("Out of Time");
                UnloadLevel();
            }
            else
            {
                Debug.Log("Loading Level");
                if (sceneIndex < Scenes.Length)
                {
                    loadSceneOp = SceneManager.LoadSceneAsync(Scenes[sceneIndex], LoadSceneMode.Additive);
                }
            }
        }
    }

    public void NextLevel()
    {
        Debug.Log("Next Level");
        UnloadLevel();
        sceneIndex++;
    }

    private void UnloadLevel()
    {
        Debug.Log("Unloading Level");
        SceneManager.UnloadSceneAsync(Scenes[sceneIndex]);
        sceneLoaded = false;
        nextTime = Time.time + TimeBetweenScenes;
    }
}
