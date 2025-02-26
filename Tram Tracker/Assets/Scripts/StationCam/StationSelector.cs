// using UnityEngine;
// using UnityEngine.SceneManagement; // Required for scene management
// using Unity.Cinemachine; // Optional, if you have Cinemachine related references, otherwise you can remove it

// public class StationSelector : MonoBehaviour
// {
//     // Reference to Cinemachine virtual cameras (Assign these in Inspector if needed)
//     public CinemachineCamera MSMStationCam; 
//     public CinemachineCamera ITStationCam;   
//     public CinemachineCamera AUMallStationCam; 
//     public CinemachineCamera QueenOfShebaStationCam; 

//     public void SelectStation(string stationName)
//     {
//         // Store the selected station name in PlayerPrefs
//         PlayerPrefs.SetString("SelectedStation", stationName);

//         // Load the UniversityMapScene
//         SceneManager.LoadScene("UniversityMapScene");
//     }
// }

using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene transitions

public class StationSelector : MonoBehaviour
{
    // Method to select a station
    public void SelectStation(string stationName)
    {
        // Store the selected station name in PlayerPrefs
        PlayerPrefs.SetString("SelectedStation", stationName);

        // Load the UniversityMapScene
        SceneManager.LoadScene("UniversityMapScene");
    }
}