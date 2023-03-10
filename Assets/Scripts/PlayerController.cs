using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public Transform orientation;

    public float moveSpeed;

    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;

    bool readyToJump;

    public float walkSpeed;
    public float sprintSpeed;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Ground Check")]
    public float _playerHeight;
    Vector3 moveDirection;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        readyToJump = true;
    }

    private void Update()
    {
        MovePlayer();
        SpeedControl();
        GroundCheck();

        // handle drag
        if (GroundCheck())
            rb.drag = groundDrag;
        else
            rb.drag = 0;
    }

    private void MovePlayer()
    {
        Vector2 input;
        
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        // when to jump
        if (Input.GetKey(jumpKey) && readyToJump && GroundCheck())
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }

        // calculate movement direction
        moveDirection = orientation.forward * input.x + orientation.right * input.y;

        // on ground movement
        if (GroundCheck())
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        // in air movement
        else if (!GroundCheck())
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }

    private bool IsOnSlope()
    {
        
    }

    private float Get

    /// <summary> 
    /// returns the surface below the player
    /// </summary>
    private RaycastHit GetGroundHit()
    {
        Physics.Raycast(transform.position, Vector3.down, out RaycastHit hitInfo);
        return hitInfo;
    }

    /// <summary>
    /// Returns if the player is grounded or not
    /// </summary>
    private bool GroundCheck()
    {
        return Physics.Raycast(transform.position, Vector3.down, _playerHeight * 0.5f + 0.1f);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // limit velocity if needed
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        // reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    private void ResetJump()
    {
        readyToJump = true;
    }
}
