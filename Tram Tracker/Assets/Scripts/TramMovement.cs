using UnityEngine;

public class TramMovement : MonoBehaviour
{
    public Transform[] stations;  // List of stations in order
    private int currentStationIndex = -1; // Start before the first station

    private void OnTriggerEnter(Collider other)
    {
        // Check if tram enters a station collider
        for (int i = 0; i < stations.Length; i++)
        {
            if (other.transform == stations[i])
            {
                currentStationIndex = i;
                SetNextStation();
            }
        }
    }

    private void SetNextStation()
    {
        if (currentStationIndex + 1 < stations.Length)
        {
            TramLocation.Instance.SetNextStation(stations[currentStationIndex + 1].name);
        }
        else
        {
            TramLocation.Instance.SetNextStation("End of Line");
        }
    }
}
