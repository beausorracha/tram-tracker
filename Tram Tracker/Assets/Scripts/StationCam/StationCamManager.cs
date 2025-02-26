using UnityEngine;
using Unity.Cinemachine;

public class StationCamManager : MonoBehaviour
{
    public CinemachineCamera MSMStationCam;
    public CinemachineCamera ITStationCam;
    public CinemachineCamera AUMallStationCam;
    public CinemachineCamera QueenOfShebaStationCam;

    void Start()
    {
        // Retrieve the selected station from PlayerPrefs
        string selectedStation = PlayerPrefs.GetString("SelectedStation", "");

        // Disable all cameras at the start
        MSMStationCam.Priority = 0;
        ITStationCam.Priority = 0;
        AUMallStationCam.Priority = 0;
        QueenOfShebaStationCam.Priority = 0;

        // Activate the correct camera based on the selected station
        switch (selectedStation)
        {
            case "MSM":
                MSMStationCam.Priority = 1;
                break;
            case "IT":
                ITStationCam.Priority = 1;
                break;
            case "AU Mall":
                AUMallStationCam.Priority = 1;
                break;
            case "Queen of Sheba":
                QueenOfShebaStationCam.Priority = 1;
                break;
            default:
                Debug.LogWarning("No valid station selected.");
                break;
        }
    }
}