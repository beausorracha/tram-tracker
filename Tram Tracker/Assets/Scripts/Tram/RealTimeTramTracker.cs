using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class RealTimeTramTracker : MonoBehaviour
{
    [Header("Tram Settings")]
    public RedisManager redisManager; // Manages connection to Redis server
    public GPSConverter gpsConverter; // Converts GPS coordinates to Unity world space
    public Transform tram; // The tram object to move
    public float moveSpeed = 10f; // Speed of tram movement
    public float updateInterval = 2f; // Interval to fetch new GPS data

    private Vector2 lastGPSPosition = Vector2.zero; // Stores the last GPS position
    private bool isMoving = false; // Tracks if the tram is currently moving

    void Start()
    {
        if (tram == null || redisManager == null || gpsConverter == null)
        {
            Debug.LogError("🚨 Missing required components! Check Inspector.");
            return;
        }

        StartCoroutine(QuickUpdateTramPosition());
    }

    IEnumerator QuickUpdateTramPosition()
    {
        Task<Vector2> fetchTask = redisManager.FetchGPSData();
        yield return new WaitUntil(() => fetchTask.IsCompleted);

        if (fetchTask.IsFaulted)
        {
            Debug.LogError("❌ Failed to fetch initial GPS data.");
            yield break;
        }

        Vector2 initialGPSPosition = fetchTask.Result;
        if (initialGPSPosition == Vector2.zero)
        {
            Debug.LogWarning("⚠️ No valid initial GPS data received.");
            yield break;
        }

        tram.position = gpsConverter.ConvertGPSToUnity(initialGPSPosition.x, initialGPSPosition.y);
        lastGPSPosition = initialGPSPosition;

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

        if (fetchTask.IsFaulted)
        {
            Debug.LogError("❌ Failed to fetch GPS data.");
            yield break;
        }

        Vector2 newGPSPosition = fetchTask.Result;
        if (newGPSPosition == Vector2.zero)
        {
            isMoving = false;
            yield break;
        }

        if (!ApproximatelyEqual(newGPSPosition, lastGPSPosition, 0.0001f))
        {
            lastGPSPosition = newGPSPosition;
            isMoving = true;
            StartCoroutine(SmoothMoveTram(gpsConverter.ConvertGPSToUnity(newGPSPosition.x, newGPSPosition.y)));
        }
        else
        {
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

            // Correct rotation fix to face the movement direction
            Vector3 direction = (targetPosition - tram.position).normalized;
            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up) * Quaternion.Euler(0, -90, 0);
                tram.rotation = Quaternion.Slerp(tram.rotation, targetRotation, Time.deltaTime * 5f);

            }

            yield return null;
        }

        tram.position = targetPosition;
        isMoving = false;
    }

    private bool ApproximatelyEqual(Vector2 a, Vector2 b, float tolerance = 0.0001f)
    {
        return Mathf.Abs(a.x - b.x) < tolerance && Mathf.Abs(a.y - b.y) < tolerance;
    }
}
