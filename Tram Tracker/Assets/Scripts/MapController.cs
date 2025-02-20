
// using UnityEngine;
// using Unity.Cinemachine;

// public class MapController : MonoBehaviour
// {
//     public CinemachineCamera mapCam; // Reference to MapCam
//     public float zoomSpeed = 0.5f; // Speed of zooming
//     public float minZoom = 0.0f; // Minimum zoom level
//     public float maxZoom = 10.0f; // Maximum zoom level

//     private Vector3 lastTouchPosition;
//     private bool isDragging = false;

//     private void Update()
//     {
//         // Handle dragging
//         HandleDragging();

//         // Handle zooming with pinch gestures
//         HandleZoom();
//     }

//     private void HandleDragging()
//     {
//         if (Input.touchCount == 1) // Ensure there's one touch
//         {
//             Touch touch = Input.GetTouch(0);

//             if (touch.phase == TouchPhase.Began)
//             {
//                 // Record the initial position
//                 lastTouchPosition = touch.position;
//                 isDragging = true; // Set dragging state
//             }
//             else if (touch.phase == TouchPhase.Moved && isDragging)
//             {
//                 // Calculate the movement delta
//                 Vector3 deltaPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, Camera.main.nearClipPlane)) 
//                                         - Camera.main.ScreenToWorldPoint(new Vector3(lastTouchPosition.x, lastTouchPosition.y, Camera.main.nearClipPlane));
//                 lastTouchPosition = touch.position;

//                 // Move the camera based on the input
//                 mapCam.transform.Translate(-deltaPosition, Space.World); // Negate to move in the correct direction
//             }
//             else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
//             {
//                 isDragging = false; // Reset dragging state
//             }
//         }
//     }

//     private void HandleZoom()
//     {
//         if (Input.touchCount == 2)
//         {
//             Touch touch0 = Input.GetTouch(0);
//             Touch touch1 = Input.GetTouch(1);

//             if (touch0.phase == TouchPhase.Moved || touch1.phase == TouchPhase.Moved)
//             {
//                 // Calculate the distance between the touches
//                 float previousDistance = Vector2.Distance(touch0.position - touch0.deltaPosition, touch1.position - touch1.deltaPosition);
//                 float currentDistance = Vector2.Distance(touch0.position, touch1.position);
//                 float zoomDelta = previousDistance - currentDistance;

//                 // Zoom in on pinch out (i.e., zoomDelta > 0 reduces height)
//                 Vector3 newPosition = mapCam.transform.position - mapCam.transform.forward * zoomDelta * zoomSpeed;
//                 float newHeight = Mathf.Clamp(newPosition.y, minZoom, maxZoom); // Limit height for zoom

//                 // Set the new position
//                 mapCam.transform.position = new Vector3(newPosition.x, newHeight, newPosition.z);
//             }
//         }
//     }
// }

using UnityEngine;
using Unity.Cinemachine;

public class MapController : MonoBehaviour
{
    public CinemachineCamera mapCam; // Reference to MapCam
    public float zoomSpeed = 0.05f; // Speed of zooming
    public float minZoom = 0.0f; // Minimum zoom level
    public float maxZoom = 10.0f; // Maximum zoom level

    private Vector3 lastTouchPosition; // To store the last touch position
    private bool isDragging = false; // To track if the user is dragging

    private void Update()
    {
        // Handle dragging
        HandleDragging();

        // Handle zooming with pinch gestures
        HandleZoom();
    }

    private void HandleDragging()
    {
        if (Input.touchCount == 1) // Ensure there's one touch
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                // Record the initial position
                lastTouchPosition = touch.position;
                isDragging = true; // Set dragging state
            }
            else if (touch.phase == TouchPhase.Moved && isDragging)
            {
                // Calculate the movement delta
                Vector3 deltaPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, Camera.main.nearClipPlane)) 
                                        - Camera.main.ScreenToWorldPoint(new Vector3(lastTouchPosition.x, lastTouchPosition.y, Camera.main.nearClipPlane));
                lastTouchPosition = touch.position;

                // Move the camera based on the input
                mapCam.transform.Translate(-deltaPosition, Space.World); // Negate to move in the correct direction
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                isDragging = false; // Reset dragging state
            }
        }
    }

    private void HandleZoom()
    {
        if (Input.touchCount == 2)
        {
            Touch touch0 = Input.GetTouch(0);
            Touch touch1 = Input.GetTouch(1);

            if (touch0.phase == TouchPhase.Moved || touch1.phase == TouchPhase.Moved)
            {
                // Calculate the distance between the touches
                float previousDistance = Vector2.Distance(touch0.position - touch0.deltaPosition, touch1.position - touch1.deltaPosition);
                float currentDistance = Vector2.Distance(touch0.position, touch1.position);
                float zoomDelta = previousDistance - currentDistance;

                // Zoom in on pinch out (i.e., zoomDelta > 0 reduces height)
                Vector3 newPosition = mapCam.transform.position - mapCam.transform.forward * zoomDelta * zoomSpeed;
                float newHeight = Mathf.Clamp(newPosition.y, minZoom, maxZoom); // Limit height for zoom

                // Set the new position
                mapCam.transform.position = new Vector3(newPosition.x, newHeight, newPosition.z);
            }
        }
    }
}