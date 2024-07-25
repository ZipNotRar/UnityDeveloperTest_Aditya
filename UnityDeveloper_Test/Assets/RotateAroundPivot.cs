using UnityEngine;

public class RotateAroundPivot : MonoBehaviour
{
    public Transform pivot; // Reference to the pivot GameObject
    public Vector3 rotationAxis = Vector3.up; // Axis of rotation
    public float rotationSpeed = 45f; // Speed of rotation in degrees per second

    void Update()
    {
        // Rotate around the pivot
        pivot.Rotate(rotationAxis, rotationSpeed * Time.deltaTime, Space.World);
    }
}
