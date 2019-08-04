using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CoinCollector : MonoBehaviour
{
    private HashSet<string> collectedCoins = new HashSet<string>();

    private SceneController sceneController;
    private GameObject hud;

    // Start is called before the first frame update
    void Start()
    {
        sceneController = GameObject.FindGameObjectWithTag("GameController").GetComponent<SceneController>();
        SceneManager.sceneUnloaded += OnSceneUnload;
        hud = GameObject.FindGameObjectWithTag("HUD");
    }

    private void OnSceneUnload(Scene arg0)
    {
        collectedCoins.Clear();
        Coin.Colors.Clear();
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
            if (collectedCoins.Contains(coin.Color))
            {
                sceneController.RestartLevel();
            }
            else
            {
                collectedCoins.Add(coin.Color);
                hud.BroadcastMessage("OnCoinPickup", coin.Color);
            }

            if (collectedCoins.Count == Coin.Colors.Count)
            {
                sceneController.NextLevel();
            }

            Destroy(coin.gameObject);
        }
    }
}
