using UnityEngine;
using UnityEngine.UI;

public class NotificationOpener : MonoBehaviour
{
    public GameObject Panel; // Assign the modal panel in Inspector
    public Button confirmButton; // Assign Confirm button in Inspector
    private string selectedStation = null; // Track selected station

    private void Start()
    {
        confirmButton.interactable = false; // Disable Confirm initially
    }

    public void SelectStation(string stationName)
    {
        selectedStation = stationName;
        PlayerPrefs.SetString("SelectedStation", stationName); // Save selection
        PlayerPrefs.Save();
        Debug.Log("🚉 Selected Station: " + stationName);

        confirmButton.interactable = true; // Enable Confirm button after selection
    }

    public void OpenNotification()
    {
        if (!string.IsNullOrEmpty(selectedStation))
        {
            if (Panel != null)
            {
                Panel.SetActive(true);
                Debug.Log("✅ Confirmation Modal Opened");
            }
        }
        else
        {
            Debug.LogWarning("⚠️ Please select a station first!");
        }
    }

    public void CloseNotification()
    {
        if (Panel != null)
        {
            Panel.SetActive(false);
        }
    }
}
