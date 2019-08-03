using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public static HashSet<string> AllDescriptors = new HashSet<string>();

    public string[] Descriptors;

    // Start is called before the first frame update
    void Start()
    {
        foreach (string descriptor in Descriptors)
        {
            AllDescriptors.Add(descriptor);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
