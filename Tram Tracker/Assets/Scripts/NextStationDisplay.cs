using UnityEngine;
using TMPro;

public class NextStationDisplay : MonoBehaviour
{
    public TMP_Text stationText;  
    public TMP_Text arrivalTimeText;  

    private void Update()
    {
        if (TramLocation.Instance != null)
        {
            stationText.text = "Next Station: " + TramLocation.Instance.nextStation;

            // Convert ETA to minutes & seconds
            float eta = TramLocation.Instance.estimatedArrivalTime;
            int minutes = Mathf.FloorToInt(eta / 60);
            int seconds = Mathf.FloorToInt(eta % 60);

            arrivalTimeText.text = "Arrives In: " + minutes + "m " + seconds + "s";
        }
    }
}
