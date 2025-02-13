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

    public void SetNextStation(string stationName, float arrivalTime)
    {
        nextStation = stationName;  
        estimatedArrivalTime = arrivalTime;  
    }
}
