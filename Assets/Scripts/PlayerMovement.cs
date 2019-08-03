using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float MovementSpeed = 5;
    public float JumpSpeed = 5;
    public float TurnSpeed = 5;

    private int groundCount;

    private new Rigidbody rigidbody;
    private Transform camera;

    public bool OnGround { get { return groundCount > 0; } }

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
        if (direction.sqrMagnitude > Mathf.Epsilon)
        {
            transform.forward = Vector3.RotateTowards(transform.forward, direction, TurnSpeed * Time.deltaTime, 0);
            Vector3 movementDelta = MovementSpeed * Time.deltaTime * transform.forward;
            rigidbody.MovePosition(transform.position + movementDelta);
        }

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
