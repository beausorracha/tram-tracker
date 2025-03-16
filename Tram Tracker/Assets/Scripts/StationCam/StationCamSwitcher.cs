using UnityEngine;
using Unity.Cinemachine;

public class CameraSwitcher : MonoBehaviour
{
    public CinemachineCamera defaultCamera; // Set to either TramCam or MapCam
    public CinemachineCamera MSMStationCam;
    public CinemachineCamera ITStationCam;
    public CinemachineCamera AUMallStationCam;
    public CinemachineCamera QueenOfShebaStationCam;

    private CinemachineCamera currentActiveCamera;

    void Start()
    {
        // Start with the default camera active
        ActivateCamera(defaultCamera);
    }

    public void ActivateCamera(CinemachineCamera newActiveCamera)
    {
        if (currentActiveCamera != null)
        {
            currentActiveCamera.gameObject.SetActive(false);
        }

        currentActiveCamera = newActiveCamera;
        currentActiveCamera.gameObject.SetActive(true);
    }

    // Methods to be called from the modal when a station is selected
    public void SelectMSMStation()
    {
        ActivateCamera(MSMStationCam);
    }

    public void SelectITStation()
    {
        ActivateCamera(ITStationCam);
    }

    public void SelectAUMallStation()
    {
        ActivateCamera(AUMallStationCam);
    }

    public void SelectQueenOfShebaStation()
    {
        ActivateCamera(QueenOfShebaStationCam);
    }

    // Method to switch back to the default camera
    public void SwitchToDefaultCamera()
    {
        ActivateCamera(defaultCamera);
    }
}