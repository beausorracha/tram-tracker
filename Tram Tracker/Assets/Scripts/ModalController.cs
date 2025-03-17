using UnityEngine;
using TMPro;

public class ModalController : MonoBehaviour
{
    public GameObject tramStationModal; 
    public TextMeshProUGUI ObjectName; // Reference to the name text object
    public TextMeshProUGUI ObjectDetails; // Reference to the details text object
    public GameObject SettingsButton;
    public GameObject[] stationButtons; // Reference to the buttons that will be shown/hidden

    // Call this method with a parameter to determine which station the user clicked
    public void ShowModal(int stationIndex)
    {
        // Activate the modal
        tramStationModal.SetActive(true);
        
        // Clear all previous station buttons and ensure all relevant buttons are visible
        foreach (var button in stationButtons)
        {
            button.SetActive(false); // Set all buttons to inactive first
        }

        // Activate all station buttons or specific logic
        foreach (GameObject button in stationButtons)
        {
            button.SetActive(true); // Show all station buttons
        }

        // Set the relevant text for the modal
        ObjectName.gameObject.SetActive(true);
        ObjectDetails.gameObject.SetActive(true);
        
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
    }

    public void HideModal()
    {
        // Deactivate the modal
        tramStationModal.SetActive(false);
        ObjectName.gameObject.SetActive(true);
        ObjectDetails.gameObject.SetActive(true);
        SettingsButton.SetActive(false);
    }
}