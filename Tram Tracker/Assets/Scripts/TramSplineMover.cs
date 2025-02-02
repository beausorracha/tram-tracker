using UnityEngine;
using UnityEngine.Splines;

public class TramSplineMover : MonoBehaviour
{
    public SplineContainer splineContainer; // Assign in Inspector
    public float speed = 1f; // Adjust tram speed
    private float progress = 0f; // Tracks tram movement along the spline

    void Update()
    {
        if (splineContainer == null) return;

        // 🚀 Keyboard Controls for Testing
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            progress += speed * Time.deltaTime; // Move forward
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            progress -= speed * Time.deltaTime; // Move backward
        }

        progress = Mathf.Clamp01(progress); // Keep within 0-1 range

        // Update tram position along the spline
        Vector3 newPosition = splineContainer.EvaluatePosition(progress);
        transform.position = newPosition;

        // Orient tram to face forward along the spline
        transform.rotation = Quaternion.LookRotation(splineContainer.EvaluateTangent(progress));
    }
}
