using UnityEngine;

public class FollowTram : MonoBehaviour
{
    public Transform tram; // Assign the Tram GameObject in the Inspector
    public Vector3 offset = new Vector3(0, 5, -10); // Adjust camera position
    public float smoothSpeed = 5f; // Adjust for smoother movement

    void LateUpdate()
    {
        if (tram == null) return;

        // Target position based on tram position + offset
        Vector3 targetPosition = tram.position + offset;

        // Smoothly move camera to follow the tram
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);

        // Make the camera look at the tram
        transform.LookAt(tram);
    }
}
