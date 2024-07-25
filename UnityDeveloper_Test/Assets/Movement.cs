using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public CharacterController controller;
    public Animator animator;

    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;

    public float rotationSpeed;
    public LayerMask groundMask;
    public Transform worldRotationTransform; // Reference to the WorldRotation GameObject
    public Transform HaloGravityTransform; // Reference to the Halo GameObject

    private bool shouldRotateWorld = false; // Flag to check if world rotation is needed

    private Vector3 velocity;
    private bool isGrounded;

    void Update()
    {
        // Check if the player is grounded
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // Reset velocity when grounded
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Get movement input from the user
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Calculate movement direction
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        // Set animator parameters for movement
        float moveSpeed = move.magnitude;
        animator.SetFloat("Speed", moveSpeed);
        animator.SetBool("IsGrounded", isGrounded);

        // Handle jump input
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Handle world rotation inputs
        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
        {
            HaloGravityTransform.Rotate(Vector3.right * rotationSpeed);
            shouldRotateWorld = true;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && isGrounded)
        {
            HaloGravityTransform.Rotate(Vector3.left * rotationSpeed);
            shouldRotateWorld = true;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && isGrounded)
        {
            HaloGravityTransform.Rotate(Vector3.forward * rotationSpeed);
            shouldRotateWorld = true;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && isGrounded)
        {
            HaloGravityTransform.Rotate(Vector3.back * rotationSpeed);
            shouldRotateWorld = true;
        }

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // Check for enter key press to rotate the world
        if (shouldRotateWorld && Input.GetKeyDown(KeyCode.Return)) // Use KeyCode.Return for the enter key
        {
            RotateWorldTransform();
            shouldRotateWorld = false; // Reset the flag after rotation
        }

        // Debugging: Log rotation of HaloGravityTransform and worldRotationTransform
        //Debug.Log("HaloGravityTransform Rotation: " + HaloGravityTransform.rotation.eulerAngles);
        //Debug.Log("WorldRotationTransform Rotation: " + worldRotationTransform.rotation.eulerAngles);
    }

    void RotateWorldTransform()
    {
        // Apply the current rotation of HaloGravityTransform to worldRotationTransform
        worldRotationTransform.rotation = HaloGravityTransform.rotation;
        // Optionally log the result to verify the rotation
        Debug.Log("Applied HaloGravityTransform Rotation to WorldRotationTransform: " + worldRotationTransform.rotation.eulerAngles);
    }
}