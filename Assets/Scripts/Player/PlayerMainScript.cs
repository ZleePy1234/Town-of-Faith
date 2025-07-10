using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMainScript : MonoBehaviour
{
    [Header("Movement")]

    [Space(10)]
    [Range(0, 10)]
    public float walkSpeed;
    [Range(0, 10)]
    public float sprintSpeed;
    public float moveSpeed { get; private set; }
    public float dashBoostSpeed = 2f; // Speed boost during dash
    public Transform orientation;

    float horizontal;
    float vertical;

    Vector3 moveDirection;
    Rigidbody rb;
    [Range(0, 10)]
    public float groundDrag;
    public float playerHeight = 2f;
    public LayerMask groundLayer;
    bool grounded;
    [Range(0, 10)]
    public float jumpForce;
    public float jumpCooldown;
    [Range(0, 10)]
    public float airMultiplier;
    bool readyToJump;

    public enum MovementState { walking, sprinting, air };
    public MovementState state;
    public KeyCode sprintKey;
    public KeyCode jumpKey;
    public KeyCode dashKey;
    public KeyCode interactKey;
    [Space(10)]

    [Header("Stats")]
    [Space(10)]
    public int healthMax;
    public int healthCurrent;
    public int armorMax;
    public int armorCurrent;


    private void Inputs()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        if (grounded && readyToJump && Input.GetKey(KeyCode.Space))
        {
            Debug.Log("Jumping");
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }
        Dash();
    }
    private void MovePlayer()
    {
        moveDirection = orientation.forward * vertical + orientation.right * horizontal;
        if (grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }
        else if (!grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }
    }
    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    private void ResetJump()
    {
        readyToJump = true;
    }
    private void Dash()
    {
        // Implement dash functionality here if needed
        if(Input.GetKeyDown(dashKey))
        {
            Vector3 dashDirection = moveDirection.normalized * moveSpeed * dashBoostSpeed; // Example dash speed
            rb.AddForce(dashDirection, ForceMode.Impulse);
        }
    }
    private void StateHandler()
    {
        if (grounded && Input.GetKey(sprintKey))
        {
            state = MovementState.sprinting;
            moveSpeed = sprintSpeed;
        }
        else if (grounded)
        {
            state = MovementState.walking;
            moveSpeed = walkSpeed;
        }
        else
        {
            state = MovementState.air;
        }
    }
    void Awake()
    {
        readyToJump = true;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }
    void FixedUpdate()
    {
        MovePlayer();
    }
    void Update()
    {
        SpeedControl();
        StateHandler();
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, groundLayer);
        if (grounded)
        {
            rb.linearDamping = groundDrag;
        }
        else
        {
            rb.linearDamping = 0;
        }
        Inputs();
    }
    
}
