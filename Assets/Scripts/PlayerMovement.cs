using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float MovementSpeed;
    public float JumpSpeed;
    public bool OnGround { get { return groundCount > 0; } }

    private new Rigidbody rigidbody;
    private int groundCount;
    
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float movementDelta = MovementSpeed * Time.deltaTime * horizontal;
        rigidbody.MovePosition(transform.position + transform.right * movementDelta);

        bool jump = Input.GetButtonDown("Jump");
        if (jump && OnGround)
        {
            Debug.Log("Jump");
            rigidbody.velocity += transform.up * JumpSpeed;
        }
    }

    public void AddGround()
    {
        Debug.Log("Received");
        ++groundCount;
    }

    public void RemoveGround()
    {
        --groundCount;
    }
}
