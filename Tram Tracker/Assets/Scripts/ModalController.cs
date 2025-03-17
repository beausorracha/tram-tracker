using UnityEngine;
using TMPro;

public class ModalController : MonoBehaviour
{
    public GameObject tramStationModal; // Reference to the TramStationModal GameObject
    public TextMeshProUGUI ObjectName; // Reference to the name text object
    public TextMeshProUGUI ObjectDetails; // Reference to the details text object
    public GameObject ExitButton;
    public GameObject SettingsButton;
    public GameObject RefreshButton;
    public GameObject[] stationButtons; // Reference to the buttons that represent each station

    // Call this method to display the modal with options
    public void ShowModal()
    {
        // Activate the modal
        tramStationModal.SetActive(true);
        
        // Show all available station buttons
        foreach (var button in stationButtons)
        {
            button.SetActive(true); 
        }
    }

    // Call this method to display the specific station's details
    public void ShowStationInfo(int stationIndex)
    {
        // Hide all station buttons first if only showing info
        foreach (var button in stationButtons)
        {
            button.SetActive(false);
        }

        // Update ObjectName and ObjectDetails based on the selected station
        switch (stationIndex)
        {
            case 0: // MSM Station
                ObjectName.text = "MSM Station";
                ObjectDetails.text = "Details about MSM Station...";
                break;
            case 1: // IT Station
                ObjectName.text = "IT Station";
                ObjectDetails.text = "Details about IT Station...";
                break;
            case 2: // AUMall Station
                ObjectName.text = "AUMall Station";
                ObjectDetails.text = "Details about AUMall Station...";
                break;
            case 3: // Queen Of Sheba Station
                ObjectName.text = "Queen Of Sheba Station";
                ObjectDetails.text = "Details about Queen Of Sheba Station...";
                break;
            default:
                Debug.LogWarning("Unknown station index: " + stationIndex);
                break;
        }

        // Show the ObjectName and ObjectDetails
        ObjectName.gameObject.SetActive(true);
        ObjectDetails.gameObject.SetActive(true);
        ExitButton.SetActive(true);
        tramStationModal.SetActive(false);
        SettingsButton.SetActive(false);
        RefreshButton.SetActive(false);
    }

    public void ExitStationSelectior()
    {
        tramStationModal.SetActive(false);
        ObjectName.gameObject.SetActive(false);
        ObjectDetails.gameObject.SetActive(false);
        ExitButton.gameObject.SetActive(false);
        SettingsButton.gameObject.SetActive(true);
        RefreshButton.gameObject.SetActive(true);
    }
}