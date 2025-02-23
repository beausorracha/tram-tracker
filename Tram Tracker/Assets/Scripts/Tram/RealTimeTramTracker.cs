using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class RealTimeTramTracker : MonoBehaviour
{
    [Header("Tram Settings")]
    public RedisManager redisManager; // Redis connection manager
    public GPSConverter gpsConverter; // GPS-to-Unity coordinate converter
    public Transform tram; // Tram object
    public float moveSpeed = 12f; // Speed of tram movement
    public float updateInterval = 2f; // Interval to fetch GPS data
    public float minMoveThreshold = 0.00005f; // Prevents micro-movement due to GPS noise

    private Vector2 lastGPSPosition = Vector2.zero;
    private Coroutine moveCoroutine; // Stores movement coroutine for better control

    void Start()
    {
        if (tram == null || redisManager == null || gpsConverter == null)
        {
            Debug.LogError("Missing required components! Check Inspector.");
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
            Debug.LogError("Failed to fetch initial GPS data.");
            yield break;
        }

        Vector2 initialGPSPosition = fetchTask.Result;
        if (initialGPSPosition == Vector2.zero)
        {
            Debug.LogWarning("No valid initial GPS data received.");
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
        // Fetch GPS Data Asynchronously
        Task<Vector2> fetchTask = redisManager.FetchGPSData();
        yield return new WaitUntil(() => fetchTask.IsCompleted);

        if (fetchTask.IsFaulted)
        {
            Debug.LogError("Failed to fetch GPS data.");
            yield break;
        }

        Vector2 newGPSPosition = fetchTask.Result;
        if (newGPSPosition == Vector2.zero)
        {
            Debug.LogWarning("No valid GPS position received.");
            yield break;
        }

        // Prevent tram from stopping due to minor GPS fluctuations
        if (!HasSignificantMovement(newGPSPosition, lastGPSPosition, minMoveThreshold))
        {
            Debug.Log("Small GPS change detected. Skipping update.");
            yield break;
        }

        lastGPSPosition = newGPSPosition;

        // Convert GPS coordinates to Unity position
        Vector3 targetPosition = gpsConverter.ConvertGPSToUnity(newGPSPosition.x, newGPSPosition.y);

        // Stop any existing movement coroutine before starting a new one
        if (moveCoroutine != null)
            StopCoroutine(moveCoroutine);

        moveCoroutine = StartCoroutine(SmoothMoveTram(targetPosition));
    }

    private IEnumerator SmoothMoveTram(Vector3 targetPosition)
{
    Vector3 startPosition = tram.position;
    float totalDistance = Vector3.Distance(startPosition, targetPosition);
    float journeyTime = totalDistance / moveSpeed;
    float journey = 0f;

    while (journey < 1f) // Ensures tram moves smoothly until it reaches the target
    {
        journey += Time.deltaTime / journeyTime;
        float easeFactor = Mathf.SmoothStep(0f, 1f, journey);
        tram.position = Vector3.Lerp(startPosition, targetPosition, easeFactor);

        // Rotate tram towards movement direction dynamically
        Vector3 direction = (targetPosition - tram.position).normalized;
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up) * Quaternion.Euler(0, -90, 0);
            tram.rotation = Quaternion.Slerp(tram.rotation, targetRotation, Time.deltaTime * 5f);
        }

        yield return null;
    }

    // Ensure tram reaches exact target position
    tram.position = targetPosition; 
}


    private bool HasSignificantMovement(Vector2 newPos, Vector2 oldPos, float threshold)
    {
        return Mathf.Abs(newPos.x - oldPos.x) > threshold || Mathf.Abs(newPos.y - oldPos.y) > threshold;
    }
}
