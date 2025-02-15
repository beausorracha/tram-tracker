using System;
using UnityEngine;
using StackExchange.Redis;
using System.Threading.Tasks;
using Newtonsoft.Json;  // ✅ Use Newtonsoft.Json for flexible JSON parsing

public class RedisManager : MonoBehaviour
{
    private static ConnectionMultiplexer redis;
    private static IDatabase db;
    private static bool isConnected = false; // ✅ Track connection status

    private string redisHost = "redis-13242.crce178.ap-east-1-1.ec2.redns.redis-cloud.com";
    private int redisPort = 13242;
    private string redisPassword = "z1WTBRd81HGrGckawMz6oHtHNOoXAAR3"; 

    async void Start()
    {
        await ConnectToRedis();
        if (isConnected)
        {
            Debug.Log("✅ Redis Connected Successfully!");
            await FetchGPSData(); // Fetch GPS data immediately after connection
        }
    }

    private async Task ConnectToRedis()
    {
        try
        {
            string connectionString = $"{redisHost}:{redisPort},password={redisPassword}";
            redis = await ConnectionMultiplexer.ConnectAsync(connectionString);
            db = redis.GetDatabase();
            isConnected = true;
        }
        catch (Exception ex)
        {
            isConnected = false;
            Debug.LogError($"❌ Redis Connection Failed: {ex.Message}");
        }
    }

    public async Task<Vector2> FetchGPSData()
    {
        if (!isConnected || db == null)
        {
            Debug.LogError("❌ Redis not connected. Attempting reconnection...");
            await ConnectToRedis(); // 🔥 Try reconnecting
            if (!isConnected) return Vector2.zero;
        }

        Debug.Log("🔍 Fetching GPS data from Redis...");

        string gpsDataJson = await db.StringGetAsync("gps:tram_1");

        if (string.IsNullOrEmpty(gpsDataJson))
        {
            Debug.LogWarning("⚠️ No GPS data found in Redis!");
            return Vector2.zero;
        }

        // 🔹 Clean up double-encoded JSON if necessary
        if (gpsDataJson.StartsWith("\"") && gpsDataJson.EndsWith("\""))
        {
            Debug.Log("🔄 Detected double-encoded JSON, fixing it...");
            gpsDataJson = gpsDataJson.Trim('"').Replace("\\\"", "\"");
        }

        Debug.Log($"🟢 CLEANED JSON: {gpsDataJson}");

        GPSData gps;
        try
        {
            gps = JsonConvert.DeserializeObject<GPSData>(gpsDataJson);
        }
        catch (Exception e)
        {
            Debug.LogError($"❌ JSON Parsing Failed: {e.Message}");
            return Vector2.zero;
        }

        if (gps == null)
        {
            Debug.LogError("❌ GPS data is null after parsing.");
            return Vector2.zero;
        }

        Vector2 position = new Vector2((float)gps.latitude, (float)gps.longitude);
        Debug.Log($"📍 GPS Position: {position}");

        return position;
    }

    [Serializable]
    private class GPSData
    {
        public double latitude;
        public double longitude;
    }
}
