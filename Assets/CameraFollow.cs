using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;  // The target to follow (the last block)
    public float heightOffset = 5f;  // How far above the target the camera should be
    public float smoothSpeed = 0.125f; // Speed of the camera's movement

    private void LateUpdate()
    {
        if (target != null)
        {
            // Calculate the desired position above the target
            Vector3 desiredPosition = new Vector3(transform.position.x, target.position.y + heightOffset, transform.position.z);
            // Smoothly interpolate to the desired position
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;

            // Optional: Make the camera look at the target
            transform.LookAt(target);
        }
    }
}
