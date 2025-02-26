using UnityEngine;
using TMPro;
using System.Collections;

public class DisplayETA : MonoBehaviour
{
    public TextMeshProUGUI etaText; // Assign TextMeshPro UI in Inspector
    public float updateInterval = 1f; // Update every 1 second

    private void Start()
    {
        if (etaText == null)
        {
            Debug.LogError("🚨 ERROR: etaText (TextMeshProUGUI) is not assigned in the Inspector!");
            return;
        }

        StartCoroutine(UpdateETA()); // Start updating every 1 second
    }

    private IEnumerator UpdateETA()
    {
        while (true) // Keep updating every second
        {
            if (TramETAData.Instance != null)
            {
                float eta = TramETAData.Instance.estimatedTime;
                string nextStation = TramETAData.Instance.nextStationName;
                string tramStatus = TramETAData.Instance.tramStatus; // Moving or Stopped

                if (eta == 0)
                {
                    // 🚋 Show current station if tram is stopped
                    etaText.text = $"🚋 Tram is currently at {tramStatus}";
                    //Debug.Log($"📍 Tram Stopped at: {tramStatus}");
                }
                else
                {
                    // ⏳ Show ETA if tram is moving
                    int minutes = Mathf.FloorToInt(eta / 60);
                    int seconds = Mathf.FloorToInt(eta % 60);
                    etaText.text = $"ETA: {minutes}m {seconds}s";
                    //Debug.Log($"✅ ETA Displayed: {minutes}m {seconds}s | Next Stop: {nextStation}");
                }
            }
            else
            {
                etaText.text = "🚋 Tram data unavailable";
                //Debug.LogError("🚨 ERROR: TramETAData Instance not found in the scene!");
            }

            yield return new WaitForSeconds(updateInterval); // Wait 1 second before updating again
        }
    }
}