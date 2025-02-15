using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class RealTimeTramTracker : MonoBehaviour
{
    [Header("Tram Settings")]
    public RedisManager redisManager; // Manages connection to Redis server
    public GPSConverter gpsConverter; // Converts GPS coordinates to Unity world space
    public Transform tram; // The tram object to move
    public float moveSpeed = 50f; // Speed of tram movement
    public float updateInterval = 0.5f; // Interval to fetch new GPS data

    private Vector2 lastGPSPosition = Vector2.zero; // Stores the last GPS position
    private bool isMoving = false; // Tracks if the tram is currently moving

    void Start()
    {
        // Validate required components
        if (tram == null)
        {
            Debug.LogError("🚨 Tram Transform is not assigned in Inspector!");
            return;
        }
        if (redisManager == null)
        {
            Debug.LogError("🚨 RedisManager is not assigned in Inspector!");
            return;
        }
        if (gpsConverter == null)
        {
            Debug.LogError("🚨 GPSConverter is not assigned!");
            return;
        }

        // Start the coroutine to update tram position
        StartCoroutine(UpdateTramPosition());
    }

    IEnumerator UpdateTramPosition()
    {
        while (true)
        {
            yield return FetchAndMoveTram(); // Fetch new GPS data and move the tram
            yield return new WaitForSeconds(updateInterval); // Wait for the next update
        }
    }

    private IEnumerator FetchAndMoveTram()
    {
        // Fetch GPS data from Redis
        Task<Vector2> fetchTask = redisManager.FetchGPSData();
        yield return new WaitUntil(() => fetchTask.IsCompleted);

        // Handle task errors
        if (fetchTask.IsFaulted)
        {
            Debug.LogError("❌ Failed to fetch GPS data: " + fetchTask.Exception);
            yield break;
        }

        // Get the new GPS position
        Vector2 newGPSPosition = fetchTask.Result;
        if (newGPSPosition == Vector2.zero)
        {
            Debug.LogWarning("⚠️ No valid GPS data received. Keeping previous position.");
            isMoving = false;
            yield break;
        }

        Debug.Log($"📡 Received GPS Data: {newGPSPosition.x:F8}, {newGPSPosition.y:F8}");

        // Convert GPS to Unity world coordinates
        Vector3 targetPosition = gpsConverter.ConvertGPSToUnity(newGPSPosition.x, newGPSPosition.y);
        Debug.Log($"🎯 Converted Unity Position: {targetPosition}");

        // Check if the new position is significantly different from the last position
        if (!ApproximatelyEqual(newGPSPosition, lastGPSPosition, 0.0001f))
        {
            lastGPSPosition = newGPSPosition;
            Debug.Log($"🎯 Moving Tram to: {targetPosition.x:F8}, {targetPosition.y:F8}");
            isMoving = true;
            StartCoroutine(SmoothMoveTram(targetPosition)); // Move the tram smoothly
        }
        else
        {
            Debug.Log("🚏 GPS data is unchanged. Tram stops moving.");
            isMoving = false;
        }
    }

    private IEnumerator SmoothMoveTram(Vector3 targetPosition)
    {
        Vector3 startPosition = tram.position; // Starting position of the tram
        float totalDistance = Vector3.Distance(startPosition, targetPosition); // Distance to move
        float journeyTime = totalDistance / moveSpeed; // Time to complete the movement
        float journey = 0f; // Progress of the movement

        while (journey < 1f && isMoving)
        {
            journey += Time.deltaTime / journeyTime; // Update progress
            tram.position = Vector3.Lerp(startPosition, targetPosition, journey); // Move the tram

            // Rotate the tram to face the direction of movement
            Vector3 direction = (targetPosition - tram.position).normalized;
            if (direction != Vector3.zero)
            {
                tram.rotation = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 90, 0);
            }

            yield return null; // Wait for the next frame
        }

        // Ensure the tram reaches the exact target position
        if (journey >= 1f)
        {
            tram.position = targetPosition;
        }
    }

    private bool ApproximatelyEqual(Vector2 a, Vector2 b, float tolerance = 0.0001f)
    {
        // Check if two GPS positions are approximately equal
        return Mathf.Abs(a.x - b.x) < tolerance && Mathf.Abs(a.y - b.y) < tolerance;
    }
}