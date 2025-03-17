using UnityEngine;
using TMPro;

public class ModalController : MonoBehaviour
{
    public GameObject tramStationModal; // Modal window for selecting stations
    public TextMeshProUGUI ObjectName;   // Text for station name
    public TextMeshProUGUI ObjectDetails; // Text for station details
    public GameObject ExitButton;
    public GameObject SettingsButton;
    public GameObject RefreshButton;
    public GameObject[] stationButtons; // Buttons inside modal for each station

    public CameraSwitcher cameraSwitcher; // Reference to the CameraSwitcher script!

    // =============================
    // ======= PUBLIC BUTTONS ======
    // =============================

    // Call this when pressing the Station Button from main menu
    public void OnStationButtonClicked()
    {
        Debug.Log("Station Button Clicked");

        // JUST show the modal, no 2D switch!
        ShowModal();
    }

    // Call this when a station button is clicked inside the modal
    public void OnStationSelected(int stationIndex)
    {
        Debug.Log($"Station {stationIndex} Selected");

        // Show station info
        ShowStationInfo(stationIndex);

        // Switch to the correct camera for this station!
        SwitchStationCamera(stationIndex);
    }

    // =============================
    // ======= PRIVATE LOGIC =======
    // =============================

    // Displays the modal with station selection buttons
    private void ShowModal()
    {
        Debug.Log("Showing Station Modal");

        tramStationModal.SetActive(true);

        foreach (var button in stationButtons)
        {
            button.SetActive(true);
        }

        // SettingsButton.SetActive(true);
        // RefreshButton.SetActive(true);

        ObjectName.gameObject.SetActive(false);
        ObjectDetails.gameObject.SetActive(false);
        ExitButton.SetActive(true);
    }

    // Displays specific station details based on selection
    private void ShowStationInfo(int stationIndex)
    {
        Debug.Log($"Showing Info for Station {stationIndex}");

        foreach (var button in stationButtons)
        {
            button.SetActive(false);
        }

        switch (stationIndex)
        {
            case 0:
                ObjectName.text = "MSM Station";
                ObjectDetails.text = "Details about MSM Station...";
                break;
            case 1:
                ObjectName.text = "IT Station";
                ObjectDetails.text = "Details about IT Station...";
                break;
            case 2:
                ObjectName.text = "AUMall Station";
                ObjectDetails.text = "Details about AUMall Station...";
                break;
            case 3:
                ObjectName.text = "Queen Of Sheba Station";
                ObjectDetails.text = "Details about Queen Of Sheba Station...";
                break;
            default:
                Debug.LogWarning($"Unknown station index: {stationIndex}");
                ObjectName.text = "Unknown Station";
                ObjectDetails.text = "No details available.";
                break;
        }

        ObjectName.gameObject.SetActive(true);
        ObjectDetails.gameObject.SetActive(true);

        ExitButton.SetActive(true);
        tramStationModal.SetActive(false);

        SettingsButton.SetActive(false);
        RefreshButton.SetActive(false);
    }

    private void SwitchStationCamera(int stationIndex)
    {
        if (cameraSwitcher == null)
        {
            Debug.LogError("CameraSwitcher reference not set on ModalController!");
            return;
        }

        // Switch based on station index
        switch (stationIndex)
        {
            case 0:
                cameraSwitcher.SwitchToCamera("msm");
                break;
            case 1:
                cameraSwitcher.SwitchToCamera("it");
                break;
            case 2:
                cameraSwitcher.SwitchToCamera("aumall");
                break;
            case 3:
                cameraSwitcher.SwitchToCamera("queen");
                break;
            default:
                Debug.LogWarning($"No camera switch found for station index: {stationIndex}");
                break;
        }
    }

    // Exits station selector and resets UI
    public void ExitStationSelector()
    {
        Debug.Log("Exiting Station Selector");

        tramStationModal.SetActive(false);
        ObjectName.gameObject.SetActive(false);
        ObjectDetails.gameObject.SetActive(false);
        ExitButton.SetActive(false);

        // Restore default cameras (optional)
        //cameraSwitcher.SwitchToCamera("tram");

        SettingsButton.SetActive(true);
        RefreshButton.SetActive(true);
    }
}
