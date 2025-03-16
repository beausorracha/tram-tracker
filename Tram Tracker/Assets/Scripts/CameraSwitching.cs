// using UnityEngine;
// using Unity.Cinemachine;

// public class CameraSwitching : MonoBehaviour
// {
//     public CinemachineCamera TramCam;
//     public CinemachineCamera MapCam;

//     public void SwitchCamera()
//     {
//         // Swap priorities
//         int tempPriority = TramCam.Priority;
//         TramCam.Priority = MapCam.Priority;
//         MapCam.Priority = tempPriority;
//     }
// }

// using UnityEngine;
// using Unity.Cinemachine;

// public class CameraSwitcher : MonoBehaviour
// {
//     public CinemachineCamera TramCam; // Main tram camera
//     public CinemachineCamera MapCam; // Main map camera
//     public CinemachineCamera[] StationCams; // Array for station cameras

//     private CinemachineCamera currentActiveCamera;

//     void Start()
//     {
//         // Initialize with the TramCam or another default camera
//         ActivateCamera(TramCam);
//     }

//     // Method to activate a specific camera
//     private void ActivateCamera(CinemachineCamera newActiveCamera)
//     {
//         // Disable the currently active camera
//         if (currentActiveCamera != null)
//         {
//             currentActiveCamera.gameObject.SetActive(false);
//         }

//         // Activate the new camera and update the reference
//         currentActiveCamera = newActiveCamera;
//         currentActiveCamera.gameObject.SetActive(true);
//     }

//     // Switch to a specific camera based on the input
//     public void SwitchToCamera(string cameraType)
//     {
//         switch (cameraType.ToLower())
//         {
//             case "tram":
//                 ActivateCamera(TramCam);
//                 break;
//             case "map":
//                 ActivateCamera(MapCam);
//                 break;
//             case "msm":
//                 ActivateCamera(StationCams[0]); // Assuming MSM is at index 0
//                 break;
//             case "it":
//                 ActivateCamera(StationCams[1]); // Assuming IT is at index 1
//                 break;
//             case "aumall":
//                 ActivateCamera(StationCams[2]); // Assuming AUMall is at index 2
//                 break;
//             case "queen":
//                 ActivateCamera(StationCams[3]); // Assuming Queen Of Sheba is at index 3
//                 break;
//             default:
//                 Debug.LogWarning("Unknown camera type: " + cameraType);
//                 break;
//         }
//     }
// }

using UnityEngine;
using Unity.Cinemachine;

public class CameraSwitcher : MonoBehaviour
{
    public CinemachineCamera TramCam; // Main tram camera
    public CinemachineCamera MapCam; // Main map camera
    public CinemachineCamera[] StationCams; // Array for station cameras

    private CinemachineCamera currentActiveCamera;
    private bool isTramActive = true; // Track which camera is currently active

    void Start()
    {
        // Initialize with the TramCam as the default camera
        ActivateCamera(TramCam);
    }

    // Method to activate a specific camera
    private void ActivateCamera(CinemachineCamera newActiveCamera)
    {
        // Disable the currently active camera
        if (currentActiveCamera != null)
        {
            currentActiveCamera.gameObject.SetActive(false);
        }

        // Activate the new camera and update the reference
        currentActiveCamera = newActiveCamera;
        currentActiveCamera.gameObject.SetActive(true);
    }

    // Switch to a specific camera based on the input
    public void SwitchToCamera(string cameraType)
    {
        switch (cameraType.ToLower())
        {
            case "tram":
                ActivateCamera(TramCam);
                isTramActive = true; // Update state
                break;
            case "map":
                ActivateCamera(MapCam);
                isTramActive = false; // Update state
                break;
            case "msm":
                ActivateCamera(StationCams[0]); // Assuming MSM is at index 0
                break;
            case "it":
                ActivateCamera(StationCams[1]); // Assuming IT is at index 1
                break;
            case "aumall":
                ActivateCamera(StationCams[2]); // Assuming AUMall is at index 2
                break;
            case "queen":
                ActivateCamera(StationCams[3]); // Assuming Queen Of Sheba is at index 3
                break;
            default:
                Debug.LogWarning("Unknown camera type: " + cameraType);
                break;
        }
    }

    // Toggles between TramCam and MapCam
    public void ToggleTramMapCamera()
    {
        if (isTramActive)
        {
            ActivateCamera(MapCam);
        }
        else
        {
            ActivateCamera(TramCam);
        }
        isTramActive = !isTramActive; // Toggle the state
    }
}