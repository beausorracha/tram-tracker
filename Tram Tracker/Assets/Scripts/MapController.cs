
// using UnityEngine;
// using Unity.Cinemachine;

// public class MapController : MonoBehaviour
// {
// #if UNITY_IOS || UNITY_ANDROID
//     public CinemachineCamera MapCam; // Reference to CinemachineCamera
//     private Plane plane; // Track the plane for raycasting

//     public float zoomSpeed = 0.5f; // Adjusted zoom speed for more control
//     public float minZoom; // Minimum zoom level (camera height)
//     public float maxZoom; // Maximum zoom level (camera height)

//     private void Awake()
//     {
//         if (MapCam == null) 
//             MapCam = FindObjectOfType<CinemachineCamera>(); // Find if MapCam is not assigned
//     }

//     private void Start()
//     {
//         // Set minZoom and maxZoom based on your map's requirements
//         minZoom = 200f; // Minimum Y position (fully zoomed in)
//         maxZoom = 975f; // Maximum Y position (fully zoomed out)

//         // Create and setup the plane
//         Vector3 normal = Vector3.up; // Assuming the map is flat on the Y plane
//         plane = new Plane(normal, transform.position);
//     }

//     private void Update()
//     {
//         // Handle dragging for scrolling
//         if (Input.touchCount >= 1) 
//         {
//             HandleDragging();
//         }

//         if (Input.touchCount >= 2) 
//         {
//             HandlingPinching();
//         }
//     }

//     private void HandleDragging()
//     {
//         Touch touch = Input.GetTouch(0);
    
//         // Only move on touch if finger is down or moved
//         if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved)
//         {
//             Vector3 delta = GetPlanePositionDelta(touch);
//             if (delta != Vector3.zero)
//             {
//                 // Move the camera based on user input
//                 MapCam.transform.Translate(delta, Space.World); // Move in the correct direction
//             }
//         }
//     }

//     private void HandlingPinching() 
//     {
//         Touch touch0 = Input.GetTouch(0);
//         Touch touch1 = Input.GetTouch(1);

//         // Get the previous and current touch positions for both fingers
//         Vector2 touch0PrevPos = touch0.position - touch0.deltaPosition;
//         Vector2 touch1PrevPos = touch1.position - touch1.deltaPosition;

//         // Calculate pinch distance changes
//         float previousDistance = Vector2.Distance(touch0PrevPos, touch1PrevPos);
//         float currentDistance = Vector2.Distance(touch0.position, touch1.position);

//         // Calculate zoom factor
//         float zoomDelta = currentDistance - previousDistance; // Fix: Reverse the subtraction
//         float zoomAmount = zoomDelta * zoomSpeed; // Adjust sensitivity

//         // Calculate the new camera height based on zoom
//         float newHeight = MapCam.transform.position.y - zoomAmount; // Subtract zoomAmount to move up/down correctly

//         // Clamp the new height within the min and max zoom range
//         newHeight = Mathf.Clamp(newHeight, minZoom, maxZoom);

//         // Adjust the camera's Y position
//         MapCam.transform.position = new Vector3(
//             MapCam.transform.position.x,
//             newHeight,
//             MapCam.transform.position.z
//         );

//         // Debug the camera's Y position
//         Debug.Log("Camera Y Position: " + MapCam.transform.position.y);
//     }

//     protected Vector3 GetPlanePositionDelta(Touch touch) 
//     {
//         // Delta requires two raycasts for the touch movements
//         if (touch.phase != TouchPhase.Moved)
//             return Vector3.zero;

//         Ray rayBefore = Camera.main.ScreenPointToRay(touch.position - touch.deltaPosition);
//         Ray rayNow = Camera.main.ScreenPointToRay(touch.position);
        
//         if (plane.Raycast(rayBefore, out float enterBefore) && plane.Raycast(rayNow, out float enterNow))
//         {
//             return rayBefore.GetPoint(enterBefore) - rayNow.GetPoint(enterNow);
//         }

//         return Vector3.zero; // Return zero if rays do not hit the plane
//     }
// #endif
// }

using UnityEngine;
using Unity.Cinemachine;

public class MapController : MonoBehaviour
{
#if UNITY_IOS || UNITY_ANDROID
    public CinemachineCamera MapCam; // Reference to CinemachineCamera
    private Plane plane; // Track the plane for raycasting

    public float zoomSpeed = 0.5f; // Adjusted zoom speed for more control
    public float minZoom = 200f; // Minimum zoom level (camera height)
    public float maxZoom = 975f; // Maximum zoom level (camera height)

    // Boundaries for camera movement
    public float minX = -50f; // Minimum X position
    public float maxX = 50f;  // Maximum X position
    public float minZ = -50f; // Minimum Z position
    public float maxZ = 50f;  // Maximum Z position

    private Vector3 originalCameraPosition; // Store the original camera position

    private void Awake()
    {
        if (MapCam == null) 
            MapCam = FindObjectOfType<CinemachineCamera>(); // Find if MapCam is not assigned
    }

    private void Start()
    {
        // Store the original camera position
        originalCameraPosition = MapCam.transform.position;

        // Create and setup the plane
        Vector3 normal = Vector3.up; // Assuming the map is flat on the Y plane
        plane = new Plane(normal, transform.position);
    }

    private void Update()
    {
        // Handle dragging for scrolling
        if (Input.touchCount >= 1) 
        {
            HandleDragging();
        }

        if (Input.touchCount >= 2) 
        {
            HandlingPinching();
        }
    }

    private void HandleDragging()
    {
        Touch touch = Input.GetTouch(0);
    
        // Only move on touch if finger is down or moved
        if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved)
        {
            Vector3 delta = GetPlanePositionDelta(touch);
            if (delta != Vector3.zero)
            {
                // Move the camera based on user input
                MapCam.transform.Translate(delta, Space.World);

                // Clamp the camera's position to stay within boundaries
                ClampCameraPosition();
            }
        }
    }

    private void HandlingPinching() 
    {
        Touch touch0 = Input.GetTouch(0);
        Touch touch1 = Input.GetTouch(1);

        // Get the previous and current touch positions for both fingers
        Vector2 touch0PrevPos = touch0.position - touch0.deltaPosition;
        Vector2 touch1PrevPos = touch1.position - touch1.deltaPosition;

        // Calculate pinch distance changes
        float previousDistance = Vector2.Distance(touch0PrevPos, touch1PrevPos);
        float currentDistance = Vector2.Distance(touch0.position, touch1.position);

        // Calculate zoom factor
        float zoomDelta = currentDistance - previousDistance; // Fix: Reverse the subtraction
        float zoomAmount = zoomDelta * zoomSpeed; // Adjust sensitivity

        // Calculate the new camera height based on zoom
        float newHeight = MapCam.transform.position.y - zoomAmount; // Subtract zoomAmount to move up/down correctly

        // Clamp the new height within the min and max zoom range
        newHeight = Mathf.Clamp(newHeight, minZoom, maxZoom);

        // Adjust the camera's Y position
        MapCam.transform.position = new Vector3(
            MapCam.transform.position.x,
            newHeight,
            MapCam.transform.position.z
        );

        // Debug the camera's Y position
        Debug.Log("Camera Y Position: " + MapCam.transform.position.y);
    }

    protected Vector3 GetPlanePositionDelta(Touch touch) 
    {
        // Delta requires two raycasts for the touch movements
        if (touch.phase != TouchPhase.Moved)
            return Vector3.zero;

        Ray rayBefore = Camera.main.ScreenPointToRay(touch.position - touch.deltaPosition);
        Ray rayNow = Camera.main.ScreenPointToRay(touch.position);
        
        if (plane.Raycast(rayBefore, out float enterBefore) && plane.Raycast(rayNow, out float enterNow))
        {
            return rayBefore.GetPoint(enterBefore) - rayNow.GetPoint(enterNow);
        }

        return Vector3.zero; // Return zero if rays do not hit the plane
    }

    private void ClampCameraPosition()
    {
        // Get the current camera position
        Vector3 currentPosition = MapCam.transform.position;

        // Clamp the X and Z positions to stay within the defined boundaries
        float clampedX = Mathf.Clamp(currentPosition.x, originalCameraPosition.x + minX, originalCameraPosition.x + maxX);
        float clampedZ = Mathf.Clamp(currentPosition.z, originalCameraPosition.z + minZ, originalCameraPosition.z + maxZ);

        // Apply the clamped position
        MapCam.transform.position = new Vector3(clampedX, currentPosition.y, clampedZ);
    }
#endif
}