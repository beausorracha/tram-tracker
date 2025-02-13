using UnityEngine;
using TMPro;

public class NextStationDisplay : MonoBehaviour
{
    public TMP_Text stationText;  
    private void Update()
    {
        if (TramLocation.Instance != null)
        {
            stationText.text = "Next Station: " + TramLocation.Instance.nextStation;
        }
    }
}
