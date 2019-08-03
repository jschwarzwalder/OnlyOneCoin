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
    private Transform camera;
    
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        camera = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalAxis = Input.GetAxis("Horizontal");
        float verticalAxis = Input.GetAxis("Vertical");
        
        Vector3 forward = Vector3.ProjectOnPlane(camera.forward, transform.up).normalized;
        Vector3 right = Vector3.ProjectOnPlane(camera.right, transform.up).normalized;

        Vector3 direction = right * horizontalAxis + forward * verticalAxis;
        direction.Normalize();
        Vector3 movementDelta = MovementSpeed * Time.deltaTime * direction;

        rigidbody.MovePosition(transform.position + movementDelta);

        bool jump = Input.GetButtonDown("Jump");
        if (jump && OnGround)
        {
            rigidbody.velocity += transform.up * JumpSpeed;
        }
    }

    public void AddGround()
    {
        ++groundCount;
    }

    public void RemoveGround()
    {
        --groundCount;
    }
}
