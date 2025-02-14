using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class RealTimeTramTracker : MonoBehaviour
{
    [Header("Tram Settings")]
    public RedisManager redisManager;
    public GPSConverter gpsConverter;
    public Transform tram;
    public float moveSpeed = 10f; // Increased speed for smoother movement
    public float updateInterval = 1f; // Update every second

    private Vector2 lastGPSPosition = Vector2.zero;
    private bool isMoving = false;

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
            isMoving = false;
            yield break;
        }

        Debug.Log($"📡 Received GPS Data: {newGPSPosition.x:F8}, {newGPSPosition.y:F8}");
        
        // ✅ Move only if the GPS data has significantly changed (7 decimal precision)
        if (!ApproximatelyEqual(newGPSPosition, lastGPSPosition, 0.0000001f))
        {
            lastGPSPosition = newGPSPosition;
            float latitude = (float)newGPSPosition.x;
            float longitude = (float)newGPSPosition.y;
            Vector3 targetPosition = gpsConverter.ConvertGPSToUnity(latitude, longitude);
            Debug.Log($"🎯 Moving Tram to: {targetPosition.x:F8}, {targetPosition.y:F8}");
            isMoving = true;
            StartCoroutine(SmoothMoveTram(targetPosition));
        }
        else
        {
            Debug.Log("🚏 GPS data is unchanged. Tram stops moving.");
            isMoving = false;
        }
    }

    private IEnumerator SmoothMoveTram(Vector3 targetPosition)
    {
        Vector3 startPosition = tram.position;
        float totalDistance = Vector3.Distance(startPosition, targetPosition);
        float journeyTime = totalDistance / moveSpeed;
        float journey = 0f;

        while (journey < 1f && isMoving)
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