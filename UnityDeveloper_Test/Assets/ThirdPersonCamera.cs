using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target; // The target the camera will follow
    public float distance = 5.0f; // Distance from the target
    public float height = 2.0f; // Height above the target
    public float rotationDamping = 3.0f; // Damping for rotation
    public float heightDamping = 2.0f; // Damping for height adjustment

    public float mouseSensitivity = 100.0f; // Sensitivity for mouse input
    public float verticalMinLimit = -20f; // Minimum vertical angle
    public float verticalMaxLimit = 80f; // Maximum vertical angle

    private float currentRotationAngle = 0.0f; // Current horizontal rotation angle
    private float currentHeight = 0.0f; // Current height of the camera
    private float verticalRotation = 0.0f; // Current vertical rotation angle

    void Start()
    {
        // Initialize rotation angles based on current transform
        Vector3 angles = transform.eulerAngles;
        currentRotationAngle = angles.y;
        verticalRotation = angles.x;
    }

    void LateUpdate()
    {
        // Ensure the target is set
        if (!target)
            return;

        // Get mouse input for rotation
        float horizontalInput = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float verticalInput = -Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Update rotation angles
        currentRotationAngle += horizontalInput;
        verticalRotation = Mathf.Clamp(verticalRotation + verticalInput, verticalMinLimit, verticalMaxLimit);

        // Create rotation based on current angles
        Quaternion currentRotation = Quaternion.Euler(verticalRotation, currentRotationAngle, 0);

        // Calculate the target position of the camera
        currentHeight = Mathf.Lerp(currentHeight, target.position.y + height, heightDamping * Time.deltaTime);
        Vector3 targetPosition = target.position - currentRotation * Vector3.forward * distance;
        targetPosition.y = currentHeight;

        // Set the camera position and look at the target
        transform.position = targetPosition;
        transform.LookAt(target);
    }
}