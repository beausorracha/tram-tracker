using UnityEngine;

public class TramMovement : MonoBehaviour
{
    public Transform[] stations;  // List of stations in order
    public float detectionRange = 5f;  // Radius for detection
    public float tramSpeed = 10f;  

    private int currentStationIndex = -1;  // -1 means before the first station

    private void OnTriggerEnter(Collider other)
    {
        for (int i = 0; i < stations.Length; i++)
        {
            if (other.gameObject == stations[i].gameObject)
            {
                MoveToNextStation();
                break;
            }
        }
    }

    private void MoveToNextStation()
    {
        if (currentStationIndex + 1 < stations.Length)
        {
            currentStationIndex++;  
            string nextStationName = stations[currentStationIndex].name;

            // Calculate arrival time
            float arrivalTime = Vector3.Distance(transform.position, stations[currentStationIndex].position) / tramSpeed;

            // Update next station in TramLocation
            TramLocation.Instance.SetNextStation(nextStationName, arrivalTime);
        }
    }
}
