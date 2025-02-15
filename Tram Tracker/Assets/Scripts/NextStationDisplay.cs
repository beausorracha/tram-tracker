using UnityEngine;
using TMPro;

public class NextStationDisplay : MonoBehaviour
{
    public TMP_Text stationText;  
    private void Start()
    {
        if (TramLocation.Instance != null)
        {
            // Subscribe to the event
            TramLocation.Instance.OnNextStationChanged += UpdateStationText;
            UpdateStationText(); // Initialize UI with the current station
        }
        else
        {
            Debug.LogError("TramLocation Instance not found in the scene!");
        }
    }

    private void OnDestroy()
    {
        // Unsubscribe from the event to prevent memory leaks
        if (TramLocation.Instance != null)
        {
            TramLocation.Instance.OnNextStationChanged -= UpdateStationText;
        }
    }

    private void UpdateStationText()
    {
        stationText.text = "Next Station: " + TramLocation.Instance.nextStation;
    }
}
