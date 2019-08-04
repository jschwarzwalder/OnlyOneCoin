using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetector : MonoBehaviour
{

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
        if (other.gameObject.CompareTag("Ground"))
        {
            SendMessageUpwards("AddGround");
        }
        else if (other.gameObject.CompareTag("Ice"))
        {
            SendMessageUpwards("AddIce");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            SendMessageUpwards("RemoveGround");
        }
        else if (other.gameObject.CompareTag("Ice"))
        {
            SendMessageUpwards("RemoveIce");
        }
    }
}
