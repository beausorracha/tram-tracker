using UnityEngine;
using System.Collections;

public class TramETA : MonoBehaviour
{
    public Transform tram; // Assign the tram object
    public Transform[] stations; // List of station transforms
    public float updateInterval = 1f; // Time interval for updates
    public float minTramSpeed = 1.0f; // Minimum speed to prevent high ETA spikes
    public float smoothingFactor = 0.2f; // Smooth speed fluctuations

    private int currentStationIndex = 0;
    private float lastTramPositionX;
    private float tramSpeed = 10f; // Default speed to prevent division by zero

    private void Start()
    {
        if (tram == null || stations.Length == 0)
        {
            Debug.LogError("🚨 ERROR: Tram or stations are not assigned in the Inspector!");
            return;
        }

        StartCoroutine(UpdateETA());
    }

    private void Update()
    {
        float distanceMoved = Mathf.Abs(tram.position.x - lastTramPositionX);

        if (Time.deltaTime > 0) // Prevent division by zero
        {
            float newSpeed = distanceMoved / Time.deltaTime;

            // Apply smoothing to avoid sudden large jumps in tram speed
            tramSpeed = Mathf.Lerp(tramSpeed, newSpeed, smoothingFactor);

            // Ensure speed does not drop too low
            if (tramSpeed < minTramSpeed)
            {
                tramSpeed = minTramSpeed;
            }
        }

        lastTramPositionX = tram.position.x;
    }

    private IEnumerator UpdateETA()
    {
        while (true)
        {
            if (currentStationIndex < stations.Length)
            {
                float distance = Vector3.Distance(tram.position, stations[currentStationIndex].position);
                float estimatedTime = (tramSpeed > 0) ? distance / tramSpeed : 0;

                // Prevent excessive ETA values
                if (estimatedTime > 10 * 60) // More than 10 minutes? Cap it.
                {
                    estimatedTime = 10 * 60;
                }

                // If ETA is 0, tram is stopped
                string currentStation = (estimatedTime == 0) ? stations[currentStationIndex].name : "Moving";

                // Store ETA and next station only
                TramETAData.Instance.SetETA(estimatedTime, stations[currentStationIndex].name, currentStation);

                //Debug.Log($"📦 ETA Stored: {estimatedTime}s | Next: {stations[currentStationIndex].name} | Tram Status: {currentStation}");
            }

            yield return new WaitForSeconds(updateInterval);
        }
    }

    public void SetNextStation(int stationIndex)
    {
        if (stationIndex >= 0 && stationIndex < stations.Length)
        {
            currentStationIndex = stationIndex;
        }
    }
}