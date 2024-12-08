using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform target; // Reference to the player's transform
    [SerializeField] Vector3 offset = new Vector3(0, 2, -10); // Offset of the camera
    [SerializeField] float smoothSpeed = 0.125f; // Smoothing speed

    void LateUpdate()
    {
        if (target == null) return; // Ensure the target is assigned

        // Desired position based on target position and offset
        Vector3 desiredPosition = target.position + offset;

        // Smoothly interpolate between current and desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Update camera position
        transform.position = smoothedPosition;

        // Optionally lock rotation if the camera should always look forward
        transform.LookAt(target);
    }
}
