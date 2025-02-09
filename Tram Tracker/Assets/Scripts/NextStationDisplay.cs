using UnityEngine;
using UnityEngine.UI;

public class NextStationDisplay : MonoBehaviour
{
    public Text stationText;

    private void Update()
    {
        if (TramLocation.Instance != null)
        {
            stationText.text = "Next Station: " + TramLocation.Instance.nextStation;
        }
    }
}
