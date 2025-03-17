// using UnityEngine;
// using Unity.Cinemachine; // To use Cinemachine cameras
// using TMPro; // To use TextMeshPro
// using UnityEngine.UI;

// public class ClickableObjectController : MonoBehaviour
// {
//     public TextMeshProUGUI ObjectName; // Reference to the name text object
//     public TextMeshProUGUI ObjectDetails; // Reference to the details text object
//     public GameObject ExitButton;
//     public GameObject SettingsButton;
//     public GameObject RefreshButton;
//     public Button DimensionButton;

//     public string objectName; // Name to display (set in the Inspector for each GameObject)
//     public string objectDetails; // Details to display (set in the Inspector for each GameObject)
//     public CinemachineCamera virtualCamera; // Reference to the object's CinemachineCamera

//     private static CinemachineCamera activeCamera; // Track the currently active camera

//     void Update()
//     {
//         // Handle touch input for mobile devices
//         if (Input.touchCount > 0)
//         {
//             Touch touch = Input.GetTouch(0);
//             if (touch.phase == TouchPhase.Began)
//             {
//                 // Perform a raycast from the touch position
//                 Ray ray = Camera.main.ScreenPointToRay(touch.position);
//                 RaycastHit hit;

//                 if (Physics.Raycast(ray, out hit)) // Check if the raycast hits any object
//                 {
//                     if (hit.transform.gameObject == gameObject) // Check if this object was clicked
//                     {
//                         ShowDetails(); // Call the method to display details
//                     }
//                 }
//             }
//         }
//     }

//     void ShowDetails()
//     {
//         // Log the interaction for debugging
//         Debug.Log("Clicked on " + objectName);
        
//         // Update the UI Text elements with the object information
//         if (ObjectName != null && ObjectDetails != null)
//         {
//             // Update the text fields
//             ObjectName.text = objectName;
//             ObjectDetails.text = objectDetails;

//             // Make sure the UI elements are visible
//             ObjectName.gameObject.SetActive(true);
//             ObjectDetails.gameObject.SetActive(true);
//             ExitButton.SetActive(true);
//             SettingsButton.SetActive(false);
//             RefreshButton.SetActive(false);
//             DimensionButton.interactable = false;
//         }

//         // Switch to the object's associated camera
//         SwitchToObjectCamera();
//     }

//     void SwitchToObjectCamera()
//     {
//         if (virtualCamera != null)
//         {
//             // If there is an existing active camera, deactivate it
//             if (activeCamera != null)
//             {
//                 activeCamera.Priority = 0; // Deactivate the currently active camera
//             }

//             // Activate the clicked object's camera by raising its priority to 10
//             virtualCamera.Priority = 10;
//             activeCamera = virtualCamera; // Set the active camera to the new one
//         }
//         else
//         {
//             Debug.LogWarning("No assigned CinemachineCamera for " + name);
//         }
//     }

// }

// using UnityEngine;
// using Unity.Cinemachine;
// using TMPro;
// using UnityEngine.UI;

// public class ClickableObjectController : MonoBehaviour
// {
//     public TextMeshProUGUI ObjectName; // Reference to the name text object
//     public TextMeshProUGUI ObjectDetails; // Reference to the details text object
//     public GameObject ExitButton;
//     public GameObject SettingsButton;
//     public GameObject RefreshButton;
//     public Button DimensionButton;

//     public string objectName; // Name to display (set in the Inspector for each GameObject)
//     public string objectDetails; // Details to display (set in the Inspector for each GameObject)
//     public CinemachineCamera virtualCamera; // Reference to the object's CinemachineCamera

//     // Static reference to the currently active camera
//     private static CinemachineCamera activeCamera; 

//     void Update()
//     {
//         // Handle touch input for mobile devices
//         if (Input.touchCount > 0)
//         {
//             Touch touch = Input.GetTouch(0);

//             if (touch.phase == TouchPhase.Began)
//             {
//                 // Perform a raycast from the touch position
//                 Ray ray = Camera.main.ScreenPointToRay(touch.position);
//                 RaycastHit hit;

//                 // Check if the raycast hits any object
//                 if (Physics.Raycast(ray, out hit))
//                 {
//                     if (hit.transform.gameObject == gameObject) // Check if this object was clicked
//                     {
//                         ShowDetails(); // Call the method to display details
//                     }
//                 }
//             }
//         }
//     }

//     void ShowDetails()
//     {
//         Debug.Log("Clicked on " + objectName);
        
//         // Update the UI Text elements with the object information
//         if (ObjectName != null && ObjectDetails != null)
//         {
//             // Update the text fields with the object's details
//             ObjectName.text = objectName;
//             ObjectDetails.text = objectDetails;

//             // Make sure the UI elements are visible
//             ObjectName.gameObject.SetActive(true);
//             ObjectDetails.gameObject.SetActive(true);
//             ExitButton.SetActive(true);
//             SettingsButton.SetActive(false); // Hide settings if unnecessary
//             RefreshButton.SetActive(false); // Hide refresh button if unnecessary
//             DimensionButton.interactable = false; // Disable dimension button while in detail view
//         }

//         // Switch to the object's associated camera
//         SwitchToObjectCamera();
//     }

//     void SwitchToObjectCamera()
//     {
//         // Check if the virtual camera is assigned
//         if (virtualCamera != null)
//         {
//             // Disable currently active camera if it exists
//             if (activeCamera != null)
//             {
//                 // Deactivate previously active camera
//                 activeCamera.gameObject.SetActive(false);
//             }

//             // Activate the clicked object's camera
//             virtualCamera.gameObject.SetActive(true);
//             activeCamera = virtualCamera; // Update reference to the active camera
//         }
//         else
//         {
//             Debug.LogWarning("No assigned CinemachineCamera for " + name);
//         }
//     }
// }

using UnityEngine;
using TMPro;

public class ClickableObjectController : MonoBehaviour
{
    public TextMeshProUGUI ObjectName; // Reference to the name text object
    public TextMeshProUGUI ObjectDetails; // Reference to the details text object
    public string objectName; // Name for display
    public string objectDetails; // Details for display
    private CameraSwitcher cameraSwitcher; // Reference to your CameraSwitcher

    void Start()
    {
        // Find the CameraSwitcher component in the scene to manage camera switching
        cameraSwitcher = FindObjectOfType<CameraSwitcher>();
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                // Perform a raycast from the touch position
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.gameObject == gameObject) // Check if this object was clicked
                    {
                        ShowDetails(); // Call the method to display details
                    }
                }
            }
        }
    }

    void ShowDetails()
    {
        // Update the object's information in the UI
        ObjectName.text = objectName;
        ObjectDetails.text = objectDetails;

        // Make the UI elements visible
        ObjectName.gameObject.SetActive(true);
        ObjectDetails.gameObject.SetActive(true);

        // Call the CameraSwitcher to switch the camera
        if (cameraSwitcher != null)
        {
            // Switch to this object's camera based on its name or specified string
            cameraSwitcher.SwitchToCamera(objectName.ToLower().Replace(" ", "")); // Removes spaces and makes it lowercase
        }
    }
}