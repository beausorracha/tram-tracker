using UnityEngine;
using TMPro;

public class ModalController : MonoBehaviour
{
    public GameObject tramStationModal; 
    public TextMeshProUGUI ObjectName; // Reference to the name text object
    public TextMeshProUGUI ObjectDetails; // Reference to the details text object
    public GameObject[] stationButtons; // Reference to the buttons that will be shown/hidden

    // Call this method with a parameter to determine which station the user clicked
    public void ShowModal(int stationIndex)
    {
        // Activate the modal
        tramStationModal.SetActive(true);
        
        // Clear previous station buttons if applicable
        foreach (var button in stationButtons)
        {
            button.SetActive(false);
        }

        // Display the relevant buttons and set the appropriate texts based on the stationIndex
        switch (stationIndex)
        {
            case 0: // MSM Station
                stationButtons[stationIndex].SetActive(true); // Show button for MSM
                ObjectName.gameObject.SetActive(true);
                ObjectDetails.gameObject.SetActive(true);
                ObjectName.text = "MSM Station";
                ObjectDetails.text = "Details about MSM Station...";
                break;
            case 1: // IT Station
                stationButtons[stationIndex].SetActive(true); // Show button for IT
                ObjectName.gameObject.SetActive(true);
                ObjectDetails.gameObject.SetActive(true);
                ObjectName.text = "IT Station";
                ObjectDetails.text = "Details about IT Station...";
                break;
            case 2: // AUMall Station
                stationButtons[stationIndex].SetActive(true); // Show button for AUMall
                ObjectName.gameObject.SetActive(true);
                ObjectDetails.gameObject.SetActive(true);
                ObjectName.text = "AUMall Station";
                ObjectDetails.text = "Details about AUMall Station...";
                break;
            case 3: // Queen Of Sheba Station
                stationButtons[stationIndex].SetActive(true); // Show button for Queen
                ObjectName.gameObject.SetActive(true);
                ObjectDetails.gameObject.SetActive(true);
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
    }
}