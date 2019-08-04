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
    private Animator animator;

    private Vector3 lastMovementDirection;

    public bool OnGround { get { return groundCount > 0 || OnIce; } }
    //public bool OnGround { get { return true; } }
    public bool OnIce { get { return iceCount > 0; } }

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        camera = GameObject.FindGameObjectWithTag("MainCamera").transform;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalAxis = Input.GetAxis("Horizontal");
        float verticalAxis = Input.GetAxis("Vertical");
        
        Vector3 forward = Vector3.ProjectOnPlane(camera.forward, transform.up).normalized;
        Vector3 right = Vector3.ProjectOnPlane(camera.right, transform.up).normalized;

        animator.SetFloat("Turn", TurnSpeed * Time.deltaTime, 0.1f, Time.deltaTime);

        transform.forward = Vector3.RotateTowards(transform.forward, forward, TurnSpeed * Time.deltaTime, 0);

        Vector3 direction = right * horizontalAxis + forward * verticalAxis;
        direction.Normalize();

        animator.SetFloat("Forward", verticalAxis, 0.1f, Time.deltaTime);

        bool sneak = Input.GetButton("Sneak");
        animator.SetBool("Crouch", sneak);

        if (OnIce)
        {
            Vector3 movementDelta = MovementSpeed * Time.deltaTime * lastMovementDirection;
            rigidbody.MovePosition(transform.position + movementDelta);
        }
        else
        if (direction.sqrMagnitude > Mathf.Epsilon)
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

    public void UpdateGround()
    {
        animator.SetBool("OnGround", OnGround);
        if (!OnGround)
        {
            animator.SetFloat("Jump", rigidbody.velocity.y);
        }
    }

    public void AddIce()
    {
        ++iceCount;
        UpdateGround();
    }

    public void RemoveIce()
    {
        --iceCount;
        UpdateGround();
    }

    public void AddGround()
    {
        ++groundCount;
        UpdateGround();
    }

    public void RemoveGround()
    {
        --groundCount;
        UpdateGround();
    }
}
