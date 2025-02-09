using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NextStationDisplay : MonoBehaviour
{
    // public Text stationText;
    public TMP_Text stationText;

    private void Update()
    {
        if (TramLocation.Instance != null)
        {
            stationText.text = "Next Station: " + TramLocation.Instance.nextStation;
        }
    }
}
