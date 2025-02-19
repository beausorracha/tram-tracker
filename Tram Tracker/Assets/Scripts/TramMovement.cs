using UnityEngine;
using Unity.Notifications.iOS; // Import for iOS notifications

public class TramMovement : MonoBehaviour
{
    public Transform[] stations; // Ordered list of stations
    private int currentStationIndex = -1; // Start before the first station
    private string userSelectedStation = null; // Store user’s selected station
    private bool hasNotified = false; // Prevent duplicate notifications

    private void Start()
    {
        // Load the selected station from PlayerPrefs
        userSelectedStation = PlayerPrefs.GetString("SelectedStation", null);

        if (TramLocation.Instance == null)
        {
            Debug.LogWarning("🚋 TramLocation Instance not found! Ensure it exists in the scene.");
        }

        if (!string.IsNullOrEmpty(userSelectedStation))
        {
            Debug.Log("🎯 User selected station: " + userSelectedStation);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        for (int i = 0; i < stations.Length; i++)
        {
            if (other.transform == stations[i])
            {
                currentStationIndex = i;
                Debug.Log("🚋 Tram reached: " + stations[i].name);
                SetNextStation();
                CheckAndSendNotification();
            }
        }
    }

    private void SetNextStation()
    {
        // Move to the next station
        currentStationIndex++;

        // Loop back if it’s the last station
        if (currentStationIndex >= stations.Length)
        {
            currentStationIndex = 0;
        }

        // Update the next station
        TramLocation.Instance.SetNextStation(stations[currentStationIndex].name);
        Debug.Log("📍 Next station: " + stations[currentStationIndex].name);
    }

    private void CheckAndSendNotification()
    {
        if (string.IsNullOrEmpty(userSelectedStation)) return;

        // Determine the previous station
        int previousStationIndex = (currentStationIndex - 1 + stations.Length) % stations.Length;
        string previousStation = stations[previousStationIndex].name;

        // Notify if tram reaches the previous station before the user’s selection
        if (previousStation == userSelectedStation && !hasNotified)
        {
            Debug.Log("🔔 Sending local notification: Tram is near " + userSelectedStation);
            SendLocalNotification("🚋 The tram is near " + userSelectedStation + "! Be ready!");
            hasNotified = true; // Prevent duplicate notifications
        }
    }

    private void SendLocalNotification(string message)
    {
#if UNITY_EDITOR
        Debug.Log("🔔 [TEST] Local Notification Triggered: " + message);
#else
        var timeTrigger = new iOSNotificationTimeIntervalTrigger()
        {
            TimeInterval = new System.TimeSpan(0, 0, 2), // Notify in 2 seconds
            Repeats = false
        };

        var notification = new iOSNotification()
        {
            Identifier = "tram_alert",
            Title = "Tram Tracker",
            Body = message,
            Subtitle = "Stay Updated!",
            ShowInForeground = true,
            ForegroundPresentationOption = (PresentationOption.Alert | PresentationOption.Sound),
            Trigger = timeTrigger
        };

        iOSNotificationCenter.ScheduleNotification(notification);
#endif
    }
}
