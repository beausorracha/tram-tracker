using UnityEngine;
using System;

public class TramLocation : MonoBehaviour
{
    public static TramLocation Instance;
    public string nextStation = "Unknown";

    public event Action OnNextStationChanged;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Debug.LogWarning("Duplicate TramLocation detected. Keeping the first instance.");
            Destroy(gameObject); // Destroy the new instance, not reset the existing one.
        }
    }

    public void SetNextStation(string stationName)
    {
        nextStation = stationName;
        OnNextStationChanged?.Invoke();
    }
}