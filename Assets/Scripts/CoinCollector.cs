using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CoinCollector : MonoBehaviour
{
    private HashSet<string> collectedCoins = new HashSet<string>();

    private SceneController sceneController;

    // Start is called before the first frame update
    void Start()
    {
        sceneController = GameObject.FindGameObjectWithTag("GameController").GetComponent<SceneController>();
        SceneManager.sceneUnloaded += OnSceneUnload;
    }

    private void OnSceneUnload(Scene arg0)
    {
        collectedCoins.Clear();
        Coin.AllDescriptors.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Coin coin = other.GetComponent<Coin>();
        if (coin != null)
        {
            foreach (string descriptor in coin.Descriptors)
            {
                if (collectedCoins.Contains(descriptor))
                {
                    Debug.Log("You Lose (" + descriptor + ")");
                }
                else
                {
                    Debug.Log("Collected " + descriptor);
                    collectedCoins.Add(descriptor);
                }
            }

            if (collectedCoins.Count == Coin.AllDescriptors.Count)
            {
                Debug.Log("You Win");
                sceneController.NextLevel();
            }

            Destroy(coin.gameObject);
        }
    }
}
