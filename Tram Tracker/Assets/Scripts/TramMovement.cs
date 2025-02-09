using UnityEngine;

public class TramMovement : MonoBehaviour
{
    public Transform[] stations;
    public float detectionRange = 10f;
    public float tramSpeed = 10f; // Speed in units per second

    private void Update()
    {
        if (TramLocation.Instance != null)
        {
            // Update tram position in TramLocation
            TramLocation.Instance.UpdateTramPosition(transform.position);
        }

        // Find the closest station
        Transform nearestStation = null;
        float shortestDistance = Mathf.Infinity;

        foreach (Transform station in stations)
        {
            float distance = Vector3.Distance(transform.position, station.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                nearestStation = station;
            }
        }

        // Update next station if within detection range
        if (nearestStation != null && shortestDistance <= detectionRange)
        {
            TramLocation.Instance.SetNextStation(nearestStation.name, nearestStation.position, tramSpeed);
        }
    }
}
