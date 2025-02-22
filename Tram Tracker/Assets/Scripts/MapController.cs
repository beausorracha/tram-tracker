
// using UnityEngine;
// using Unity.Cinemachine;

// public class MapController : MonoBehaviour
// {
// #if UNITY_IOS || UNITY_ANDROID
//     public CinemachineCamera MapCam; // Reference to CinemachineCamera
//     private Plane plane; // Track the plane for raycasting
//     public float zoomSpeed = 0.5f; // Speed of zooming
//     public float minZoom = 2.0f; // Minimum zoom level
//     public float maxZoom = 10.0f; // Maximum zoom level

//     private void Awake()
//     {
//         if (MapCam == null) 
//             MapCam = FindObjectOfType<CinemachineCamera>(); // Find if MapCam is not assigned
//     }

//     private void Start()
//     {
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
//                 MapCam.transform.Translate(delta, Space.World); // Remove the negative sign
//             }
//         }
//     }

//     private void HandlingPinching() 
//     {
//         Touch touch0 = Input.GetTouch(0);
//         Touch touch1 = Input.GetTouch(1);

//         // Get the current and previous touch positions
//         Vector2 touch0PrevPos = touch0.position - touch0.deltaPosition;
//         Vector2 touch1PrevPos = touch1.position - touch1.deltaPosition;

//         // Calculate the distance between the two touches
//         float previousDistance = Vector2.Distance(touch0PrevPos, touch1PrevPos);
//         float currentDistance = Vector2.Distance(touch0.position, touch1.position);

//         // Calculate the zoom factor
//         float zoomDelta = previousDistance - currentDistance; // Positive if zooming in, negative if zooming out

//         // Calculate how much to adjust the camera
//         float zoomAmount = zoomDelta * 0.01f; // Adjust sensitivity here (scale as appropriate)

//         // Move the camera based on the amount to zoom
//         Vector3 newPosition = MapCam.transform.position - MapCam.transform.forward * zoomAmount;

//         // Optional: Clamp the camera position if necessary
//         float newHeight = Mathf.Clamp(newPosition.y, minZoom, maxZoom);

//         // Set the new camera position
//         MapCam.transform.position = new Vector3(newPosition.x, newHeight, newPosition.z); 
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
// }
// #endif


using UnityEngine;
using Unity.Cinemachine;

public class MapController : MonoBehaviour
{
#if UNITY_IOS || UNITY_ANDROID
    public CinemachineCamera MapCam; // Reference to CinemachineCamera
    private Plane plane; // Track the plane for raycasting

    public float zoomSpeed = 0.002f; // Adjusted zoom speed for more control
    public float minZoom = 50.0f; // Minimum zoom level
    public float maxZoom = 976.2663f; // Maximum zoom level

    private void Awake()
    {
        if (MapCam == null) 
            MapCam = FindObjectOfType<CinemachineCamera>(); // Find if MapCam is not assigned
    }

    private void Start()
    {
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
                MapCam.transform.Translate(delta, Space.World); // Move in the correct direction
            }
        }
    }

    private void HandlingPinching() 
    {
        Touch touch0 = Input.GetTouch(0);
        Touch touch1 = Input.GetTouch(1);

        // Previous positions for the touches
        Vector2 touch0PrevPos = touch0.position - touch0.deltaPosition;
        Vector2 touch1PrevPos = touch1.position - touch1.deltaPosition;

        // Calculate the distances between the two touches
        float previousDistance = Vector2.Distance(touch0PrevPos, touch1PrevPos);
        float currentDistance = Vector2.Distance(touch0.position, touch1.position);

        // Calculate zoom factor
        float zoomDelta = previousDistance - currentDistance;

        // Zooming logic with adjustment for vertical orientation
        Vector3 newPosition = MapCam.transform.position + MapCam.transform.forward * zoomDelta * zoomSpeed;

        // Clamp to min/max zoom levels
        float newHeight = Mathf.Clamp(newPosition.y, minZoom, maxZoom); 

        // Set the new camera position
        MapCam.transform.position = new Vector3(newPosition.x, newHeight, newPosition.z); 
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
}
#endif