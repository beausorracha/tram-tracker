using UnityEngine;

public class TramLocation : MonoBehaviour
{
    public static TramLocation Instance;
    public Vector3 tramPosition;
    public string nextStation;
    public float estimatedArrivalTime;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdateTramPosition(Vector3 newPosition)
    {
        tramPosition = newPosition;
    }

    public void SetNextStation(string stationName, Vector3 stationPosition, float tramSpeed)
    {
        nextStation = stationName;

        // Calculate Estimated Arrival Time (ETA)
        float distanceToStation = Vector3.Distance(tramPosition, stationPosition);
        estimatedArrivalTime = tramSpeed > 0 ? distanceToStation / tramSpeed : 0;
    }
}
