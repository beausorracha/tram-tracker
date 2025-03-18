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

using UnityEngine;
using Unity.Cinemachine; // To use Cinemachine cameras
using TMPro; // To use TextMeshPro
using UnityEngine.UI; // To use UI elements

public class ClickableObjectController : MonoBehaviour
{
    // UI references
    public TextMeshProUGUI ObjectName; // Reference to the name text object
    public TextMeshProUGUI ObjectDetails; // Reference to the details text object
    
    // Object information
    public string objectName; // Name to display (set in the Inspector for each GameObject)
    public string objectDetails; // Details to display (set in the Inspector for each GameObject)
    
    // UI Buttons
    public GameObject ExitButton; // Reference to Exit button
    public GameObject SettingsButton; // Reference to Settings button
    public GameObject RefreshButton; // Reference to Refresh button
    public Button DimensionButton; // Reference to Dimension button

    public CinemachineCamera virtualCamera; // Reference to the object's CinemachineCamera
    private static CinemachineCamera activeCamera; // Track the currently active camera

    void Start()
    {
        // Attempt to find a corresponding CinemachineCamera based on the object's name
        string cameraName = objectName.ToLower().Replace(" ", "") + "Camera"; // Example camera naming convention

        // Find all CinemachineCamera objects and search for the one that matches
        CinemachineCamera[] cameras = FindObjectsOfType<CinemachineCamera>();
        foreach (var camera in cameras)
        {
            if (camera.name.Equals(cameraName))
            {
                virtualCamera = camera; // Assign the matching camera
                break; // Exit the loop once we found the camera
            }
        }
    }

    void Update()
    {
        // Handle touch input for mobile devices
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                // Perform a raycast from the touch position
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;

                // Check if the raycast hits any object
                if (Physics.Raycast(ray, out hit))
                {
                    // Check if this object was clicked
                    if (hit.transform.gameObject == gameObject) 
                    {
                        ShowDetails(); // Call the method to display details
                    }
                }
            }
        }
    }

    void ShowDetails()
    {
        // Log the interaction for debugging
        Debug.Log("Clicked on " + objectName);
            
        // Update the UI Text elements with the object information
        if (ObjectName != null && ObjectDetails != null)
        {
            // Update the text fields
            ObjectName.text = objectName; // Set the name
            ObjectDetails.text = objectDetails; // Set the details
            // Make the UI elements visible
            ObjectName.gameObject.SetActive(true);
            ObjectDetails.gameObject.SetActive(true);
            ExitButton.SetActive(true); // Show Exit button
            SettingsButton.SetActive(false); // Hide Settings button if unnecessary
            RefreshButton.SetActive(false); // Hide Refresh button if unnecessary
            DimensionButton.interactable = false; // Disable Dimension button while in detail view
        }

        // Call the CameraSwitcher to switch the camera
        if (FindObjectOfType<CameraSwitcher>() != null)
        {
            CameraSwitcher cameraSwitcher = FindObjectOfType<CameraSwitcher>();
            cameraSwitcher.SwitchToCamera(objectName.ToLower().Replace(" ", "")); // Switch based on modified object name
        }
    }
    

    // Method to hide details and reset UI elements
    public void OnExitButtonClicked()
    {
        HideDetails(); // Calls method to hide details when exit button is clicked
    }

    void HideDetails()
    {
        // Hide UI details when exiting
        if (ObjectName != null)
        {
            ObjectName.gameObject.SetActive(false); // Hide Object Name text
        }
        if (ObjectDetails != null)
        {
            ObjectDetails.gameObject.SetActive(false); // Hide Object Details text
        }

        ExitButton.SetActive(false); // Hide Exit button
        SettingsButton.SetActive(true); // Show Settings button 
        RefreshButton.SetActive(true); // Show Refresh button again
        DimensionButton.interactable = true; // Re-enable Dimension button
    }
}
