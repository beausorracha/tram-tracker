using UnityEngine;

public class TramMovement : MonoBehaviour
{
    public Transform[] stations; // Ordered list of stations
    private int currentStationIndex = -1; // Start before the first station

    private void Start()
    {
        if (TramLocation.Instance == null)
        {
            Debug.LogError("TramLocation Instance not found! Make sure TramLocation exists in the scene.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Tram entered trigger: " + other.name);

        for (int i = 0; i < stations.Length; i++)
        {
            if (other.transform == stations[i])
            {
                currentStationIndex = i;
                Debug.Log("Tram reached: " + stations[i].name);
                SetNextStation();
            }
        }
    }

    private void SetNextStation()
    {
        if (TramLocation.Instance != null)
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
}
