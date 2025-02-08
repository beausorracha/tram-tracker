using UnityEngine;

public class TramMovement : MonoBehaviour
{
    public Transform[] stations; //Need to add 4 stations here!
    public float detectionRange = 10f;

    private void Update()
    {
        // Update tram position in TramLocation
        TramLocation.Instance.UpdateTramPosition(transform.position);

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
            TramLocation.Instance.SetNextStation(nearestStation.name);
        }
    }
}
