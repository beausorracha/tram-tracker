using UnityEngine;
using Unity.Cinemachine;

public class CameraSwitcher : MonoBehaviour
{
    public CinemachineCamera TramCam;
    public CinemachineCamera MapCam;
    public CinemachineCamera[] StationCams;
    private CinemachineCamera currentActiveCamera;
    private bool isTramActive = true;

    void Start()
    {
        // Disable everything first to avoid ghost cameras
        TramCam.gameObject.SetActive(false);
        MapCam.gameObject.SetActive(false);

        foreach (var cam in StationCams)
        {
            cam.gameObject.SetActive(false);
        }

        // Now activate TramCam cleanly
        ActivateCamera(TramCam);
    }

    private void ActivateCamera(CinemachineCamera newActiveCamera)
    {
        Debug.Log($"Activating Camera: {newActiveCamera.name}");

        if (currentActiveCamera != null)
        {
            currentActiveCamera.gameObject.SetActive(false);
            Debug.Log($"Deactivated Camera: {currentActiveCamera.name}");
        }

        currentActiveCamera = newActiveCamera;
        currentActiveCamera.gameObject.SetActive(true);
    }

    public void SwitchToCamera(string cameraType)
    {
        Debug.Log($"Switching to camera: {cameraType}");

        switch (cameraType.ToLower())
        {
            case "tram":
                ActivateCamera(TramCam);
                isTramActive = true;
                break;
            case "map":
                ActivateCamera(MapCam);
                isTramActive = false;
                break;
            case "msm":
                ActivateCamera(StationCams[0]);
                break;
            case "it":
                ActivateCamera(StationCams[1]);
                break;
            case "aumall":
                ActivateCamera(StationCams[2]);
                break;
            case "queen":
                ActivateCamera(StationCams[3]);
                break;
            case "dormbuilding":
                ActivateCamera(StationCams[4]);
                break;
            case "clbuilding":
                ActivateCamera(StationCams[5]);
                break;
            case "msmebuilding":
                ActivateCamera(StationCams[6]);
                break;
            case "vmesbuilding":
                ActivateCamera(StationCams[7]);
                break;
            case "slmbuilding":
                ActivateCamera(StationCams[8]);
                break;
            case "itbuilding":
                ActivateCamera(StationCams[9]);
                break;
            case "carparkingbuilding":
                ActivateCamera(StationCams[10]);
                break;
            case "arbuilding":
                ActivateCamera(StationCams[11]);
                break;
            case "cabuilding":
                ActivateCamera(StationCams[12]);
                break;
            case "vmesfountain":
                ActivateCamera(StationCams[13]);
                break;
                case "augoldenrock":
                ActivateCamera(StationCams[14]);
                break;
            default:
                Debug.LogWarning("Unknown camera type: " + cameraType);
                break;
        }
    }

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

        isTramActive = !isTramActive;
    }
}
