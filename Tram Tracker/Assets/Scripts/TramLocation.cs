using UnityEngine;
using System;

public class TramLocation : MonoBehaviour
{
    public static TramLocation Instance;
    public string nextStation = "Unknown";

    // Event to notify listeners when nextStation changes
    public event Action OnNextStationChanged;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("TramLocation Instance Created");
        }
        else
        {
            Debug.LogWarning("Duplicate TramLocation detected. Destroying extra instance.");
            Destroy(gameObject);
        }
    }

    public void SetNextStation(string stationName)
    {
        Debug.Log("Next Station Set: " + stationName);
        nextStation = stationName;

        // Notify all listeners (UI in another scene)
        OnNextStationChanged?.Invoke();
    }
}
