using UnityEngine;
using Unity.Cinemachine;

public class MapController : MonoBehaviour
{
    public CinemachineCamera mapCam; // Reference to MapCam
    public float zoomSpeed = 0.5f; // Speed of zooming
    public float minZoom = 2.0f; // Minimum zoom level
    public float maxZoom = 10.0f; // Maximum zoom level

    private void Update()
    {
        // Handle zooming with pinch gestures
        HandleZoom();
    }

    private void HandleZoom()
    {
        if (Input.touchCount == 2)
        {
            Touch touch0 = Input.GetTouch(0);
            Touch touch1 = Input.GetTouch(1);

            if (touch0.phase == TouchPhase.Moved || touch1.phase == TouchPhase.Moved)
            {
                // Calculate distance between touches
                float previousDistance = Vector2.Distance(touch0.position - touch0.deltaPosition, touch1.position - touch1.deltaPosition);
                float currentDistance = Vector2.Distance(touch0.position, touch1.position);
                float zoomDelta = previousDistance - currentDistance;

                // Adjust the camera's position for zooming
                Vector3 newPosition = mapCam.transform.position + mapCam.transform.forward * zoomDelta * zoomSpeed;
                float newHeight = Mathf.Clamp(newPosition.y, minZoom, maxZoom); // Limit height for zoom

                // Set the new position
                mapCam.transform.position = new Vector3(newPosition.x, newHeight, newPosition.z);
            }
        }
    }
}