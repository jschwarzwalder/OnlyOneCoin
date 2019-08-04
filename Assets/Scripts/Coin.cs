using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public static HashSet<string> Colors = new HashSet<string>();

    public string Color;
    public bool Collected;

    // Start is called before the first frame update
    void Start()
    {
        if (!Colors.Contains(Color))
        {
            Colors.Add(Color);
            GameObject hud = GameObject.FindGameObjectWithTag("HUD");
            hud.BroadcastMessage("OnCoinCreate", Color);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
