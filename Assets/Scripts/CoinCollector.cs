using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    private HashSet<string> collectedCoins = new HashSet<string>();

    // Start is called before the first frame update
    void Start()
    {
        
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
                    Debug.Log("You Lose");
                }
                else
                {
                    collectedCoins.Add(descriptor);
                }
            }

            if (collectedCoins.Count == Coin.AllDescriptors.Count)
            {
                Debug.Log("You Win");
            }

            Destroy(coin.gameObject);
        }
    }
}
