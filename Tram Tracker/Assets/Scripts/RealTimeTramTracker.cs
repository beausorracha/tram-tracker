using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class RealTimeTramTracker : MonoBehaviour
{
    [Header("Tram Settings")]
    public RedisManager redisManager;
    public GPSConverter gpsConverter;
    public Transform tram;
    public float moveSpeed = 5f;
    public float updateInterval = 2f; // Seconds between Redis fetches

    private Vector2 lastGPSPosition = Vector2.zero;
    private float currentMoveSpeed;

    void Start()
    {
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

        currentMoveSpeed = moveSpeed;
        
        // 🚆 Set tram to initial GPS position
        Vector3 startPosition = gpsConverter.ConvertGPSToUnity(lastGPSPosition.x, lastGPSPosition.y);
        tram.position = startPosition;
        Debug.Log($"🚆 Setting Initial Tram Position: {startPosition}");
        
        StartCoroutine(UpdateTramPosition());
    }

    IEnumerator UpdateTramPosition()
    {
        while (true)
        {
            yield return FetchAndMoveTram();
            yield return new WaitForSeconds(updateInterval);
        }
    }

    private IEnumerator FetchAndMoveTram()
    {
        Task<Vector2> fetchTask = redisManager.FetchGPSData();
        yield return new WaitUntil(() => fetchTask.IsCompleted);

        Vector2 newGPSPosition = fetchTask.Result;

        if (newGPSPosition == Vector2.zero)
        {
            Debug.LogWarning("⚠️ No valid GPS data received. Keeping previous position.");
            yield break;
        }

        Debug.Log($"📡 Received GPS Data: {newGPSPosition}");
        
        // ✅ Move only if the GPS data has significantly changed
        if (!ApproximatelyEqual(newGPSPosition, lastGPSPosition, 0.00001f))
        {
            lastGPSPosition = newGPSPosition;
            Vector3 targetPosition = gpsConverter.ConvertGPSToUnity(newGPSPosition.x, newGPSPosition.y);
            Debug.Log($"🎯 Converted Tram Position: {targetPosition}");
            currentMoveSpeed = moveSpeed; // Restore movement speed when GPS updates
            
            // 🚆 Set the tram's position directly for testing
            tram.position = targetPosition;
        }
        else
        {
            Debug.Log("🚏 GPS data is unchanged. Tram remains in place.");
            currentMoveSpeed = 0f; // Stop movement when GPS is unchanged
        }
    }

    private IEnumerator SmoothMoveTram(Vector3 targetPosition)
    {
        Vector3 startPosition = tram.position;
        float totalDistance = Vector3.Distance(startPosition, targetPosition);
        float journeyTime = totalDistance / currentMoveSpeed;
        float journey = 0f;

        while (journey < 1f && currentMoveSpeed > 0f)
        {
            journey += Time.deltaTime / journeyTime;
            tram.position = Vector3.Lerp(startPosition, targetPosition, journey);

            Vector3 direction = (targetPosition - tram.position).normalized;
            if (direction != Vector3.zero)
            {
                tram.rotation = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 90, 0);
            }

            yield return null;
        }
    }

    private bool ApproximatelyEqual(Vector2 a, Vector2 b, float tolerance)
    {
        return Mathf.Abs(a.x - b.x) < tolerance && Mathf.Abs(a.y - b.y) < tolerance;
    }
}
