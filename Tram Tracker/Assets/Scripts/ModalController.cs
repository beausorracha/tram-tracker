using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ModalController : MonoBehaviour
{
    public GameObject tramStationModal;
    public TextMeshProUGUI ObjectName;
    public TextMeshProUGUI ObjectDetails;
    public GameObject ExitButton;
    public GameObject SettingsButton;
    public GameObject RefreshButton;
    public Button DimensionButton;
    public GameObject[] stationButtons;

    public CameraSwitcher cameraSwitcher;

    public void OnStationButtonClicked()
    {
        Debug.Log("Station Button Clicked");
        ShowModal();
    }

    public void OnStationSelected(int stationIndex)
    {
        Debug.Log($"Station {stationIndex} Selected");
        ShowStationInfo(stationIndex);
        SwitchStationCamera(stationIndex);
    }

    // ✅ Dimension Button handler (added only this function)
    public void OnDimensionButtonClicked()
{
    Debug.Log("Dimension Button Clicked");

    if (cameraSwitcher == null)
    {
        Debug.LogError("CameraSwitcher reference not set on ModalController!");
        return;
    }

    cameraSwitcher.SwitchToCamera("map");

    tramStationModal.SetActive(false);
    ObjectName.gameObject.SetActive(false);
    ObjectDetails.gameObject.SetActive(false);
    ExitButton.SetActive(false);

    SettingsButton.SetActive(true);
    RefreshButton.SetActive(true);

    DimensionButton.interactable = true;
}


    public void ShowModal()
    {
        Debug.Log("Showing Station Modal");

        tramStationModal.SetActive(true);

        foreach (var button in stationButtons)
        {
            button.SetActive(true);
        }

        ObjectName.gameObject.SetActive(false);
        ObjectDetails.gameObject.SetActive(false);
        ExitButton.SetActive(true);
        DimensionButton.interactable = false;
    }

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

    public void ExitStationSelector()
    {
        Debug.Log("Exiting Station Selector");

        tramStationModal.SetActive(false);
        ObjectName.gameObject.SetActive(false);
        ObjectDetails.gameObject.SetActive(false);
        ExitButton.SetActive(false);

        SettingsButton.SetActive(true);
        RefreshButton.SetActive(true);
        DimensionButton.interactable = true;
    }
}
