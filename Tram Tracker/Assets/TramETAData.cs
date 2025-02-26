using UnityEngine;

public class TramETAData : MonoBehaviour
{
    public static TramETAData Instance { get; private set; }

    public float estimatedTime = -1; // Store ETA in seconds
    public string nextStationName = "Unknown"; // Next station
    public string tramStatus = "Moving"; // Status: Moving or Station Name

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

    public void SetETA(float eta, string nextStation, string status)
    {
        estimatedTime = eta;
        nextStationName = nextStation;
        tramStatus = status;
    }
}