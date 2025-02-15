using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class TramPositionInitializer : MonoBehaviour
{
    public RedisManager redisManager;
    public GPSConverter gpsConverter;
    public Transform tram;

    void Start()
    {
        if (tram == null || redisManager == null || gpsConverter == null)
        {
            Debug.LogError("🚨 Missing component assignments in Inspector!");
            return;
        }

        StartCoroutine(InitializeTramPosition());
    }

    IEnumerator InitializeTramPosition()
    {
        Debug.Log("🚀 Fetching initial GPS position for tram...");

        Task<Vector2> fetchTask = redisManager.FetchGPSData();
        yield return new WaitUntil(() => fetchTask.IsCompleted);

        if (fetchTask.IsFaulted)
        {
            Debug.LogError("❌ Failed to fetch initial GPS data: " + fetchTask.Exception);
            yield break;
        }

        Vector2 initialGPSPosition = fetchTask.Result;
        if (initialGPSPosition == Vector2.zero)
        {
            Debug.LogWarning("⚠️ No valid initial GPS data received. Tram will stay in place.");
            yield break;
        }

        Vector3 initialUnityPosition = gpsConverter.ConvertGPSToUnity(initialGPSPosition.x, initialGPSPosition.y);
        tram.position = initialUnityPosition;

        Debug.Log($"✅ Tram instantly positioned at {initialUnityPosition}");
    }
}
