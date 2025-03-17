using UnityEngine;
using Unity.Cinemachine; // To use Cinemachine cameras
using TMPro; // To use TextMeshPro
using UnityEngine.UI;

public class ClickableObjectController : MonoBehaviour
{
    public TextMeshProUGUI ObjectName; // Reference to the name text object
    public TextMeshProUGUI ObjectDetails; // Reference to the details text object
    public GameObject ExitButton;
    public GameObject SettingsButton;
    public GameObject RefreshButton;
    public Button DimensionButton;

    public string objectName; // Name to display (set in the Inspector for each GameObject)
    public string objectDetails; // Details to display (set in the Inspector for each GameObject)
    public CinemachineCamera virtualCamera; // Reference to the object's CinemachineCamera

    private static CinemachineCamera activeCamera; // Track the currently active camera

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

                if (Physics.Raycast(ray, out hit)) // Check if the raycast hits any object
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
        // Log the interaction for debugging
        Debug.Log("Clicked on " + objectName);
        
        // Update the UI Text elements with the object information
        if (ObjectName != null && ObjectDetails != null)
        {
            // Update the text fields
            ObjectName.text = objectName;
            ObjectDetails.text = objectDetails;

            // Make sure the UI elements are visible
            ObjectName.gameObject.SetActive(true);
            ObjectDetails.gameObject.SetActive(true);
            ExitButton.SetActive(true);
            SettingsButton.SetActive(false);
            RefreshButton.SetActive(false);
            DimensionButton.interactable = false;
        }

        // Switch to the object's associated camera
        SwitchToObjectCamera();
    }

    void SwitchToObjectCamera()
    {
        if (virtualCamera != null)
        {
            // If there is an existing active camera, deactivate it
            if (activeCamera != null)
            {
                activeCamera.Priority = 0; // Deactivate the currently active camera
            }

            // Activate the clicked object's camera by raising its priority to 10
            virtualCamera.Priority = 10;
            activeCamera = virtualCamera; // Set the active camera to the new one
        }
        else
        {
            Debug.LogWarning("No assigned CinemachineCamera for " + name);
        }
    }
        
    // public void ExitObjectSelector()
    // {
    //     Debug.Log("Exiting Object Selector");

    //     ObjectName.gameObject.SetActive(false);
    //     ObjectDetails.gameObject.SetActive(false);
    //     ExitButton.SetActive(false);

    //     SettingsButton.SetActive(true);
    //     RefreshButton.SetActive(true);
    //     DimensionButton.interactable = true;
    // }
}