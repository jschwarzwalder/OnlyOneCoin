using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float MovementSpeed = 5;
    public float JumpSpeed = 5;
    public float TurnSpeed = 5;
    public float SneakMultiplier = 0.3f;

    private int groundCount;
    private int iceCount;

    private new Rigidbody rigidbody;
    private new Transform camera;

    private Vector3 lastMovementDirection;

    public bool OnGround { get { return groundCount > 0 || OnIce; } }
    public bool OnIce { get { return iceCount > 0; } }

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

        transform.forward = Vector3.RotateTowards(transform.forward, forward, TurnSpeed * Time.deltaTime, 0);

        Vector3 direction = right * horizontalAxis + forward * verticalAxis;
        direction.Normalize();

        RaycastHit hitInfo;
        bool hit = Physics.Raycast(transform.position, direction, out hitInfo, 0.6f);

        //ignore triggers
        hit = hit && !hitInfo.collider.isTrigger;

        bool sneak = Input.GetButton("Sneak");

        if (OnIce)
        {
            Vector3 movementDelta = MovementSpeed * Time.deltaTime * lastMovementDirection;
            rigidbody.MovePosition(transform.position + movementDelta);
        }
        else
        if (!hit && direction.sqrMagnitude > Mathf.Epsilon)
        {
            Vector3 movementDelta = MovementSpeed * Time.deltaTime * direction;
            if (sneak)
            {
                rigidbody.MovePosition(transform.position + movementDelta * SneakMultiplier);
            }
            else
            {
                rigidbody.MovePosition(transform.position + movementDelta);
            }
            lastMovementDirection = direction;
        }

        bool jump = Input.GetButtonDown("Jump");
        if (jump && OnGround)
        {
            rigidbody.velocity += transform.up * JumpSpeed;
        }
    }

    public void AddIce()
    {
        ++iceCount;
    }

    public void RemoveIce()
    {
        --iceCount;
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
