using System;
using UnityEngine;
using StackExchange.Redis;
using System.Threading.Tasks;
using Newtonsoft.Json;  // ✅ Use Newtonsoft.Json for flexible JSON parsing

public class RedisManager : MonoBehaviour
{
    private static ConnectionMultiplexer redis;
    private static IDatabase db;

    private string redisHost = "redis-13242.crce178.ap-east-1-1.ec2.redns.redis-cloud.com";
    private int redisPort = 13242;
    private string redisPassword = "z1WTBRd81HGrGckawMz6oHtHNOoXAAR3"; // 🚨 Reset for security!

    async void Start()
    {
        try
        {
            await ConnectToRedis();
            Debug.Log("✅ Connected to Redis Cloud!");

            // 🔹 Fetch GPS data immediately after connecting
            await FetchGPSData();
        }
        catch (Exception ex)
        {
            Debug.LogError($"❌ Redis connection failed: {ex.Message}");
        }
    }

    private async Task ConnectToRedis()
    {
        string connectionString = $"{redisHost}:{redisPort},password={redisPassword}";
        redis = await ConnectionMultiplexer.ConnectAsync(connectionString);
        db = redis.GetDatabase();
    }

    // 🔹 Fetch GPS data and clean double-encoded JSON
    public async Task<Vector2> FetchGPSData()
    {
        if (db == null)
        {
            Debug.LogError("❌ Redis not connected.");
            return Vector2.zero;
        }

        Debug.Log("🔍 Attempting to fetch GPS data from Redis...");

        string gpsDataJson = await db.StringGetAsync("gps:tram_1");

        // 🔴 Print raw JSON for debugging
        Debug.Log($"🔴 RAW JSON FROM REDIS: {gpsDataJson}");

        if (string.IsNullOrEmpty(gpsDataJson))
        {
            Debug.LogWarning("⚠️ No GPS data found in Redis. Key: 'gps:tram_1' is empty!");
            return Vector2.zero;
        }

        // 🔹 Clean up double-encoded JSON if necessary
        if (gpsDataJson.StartsWith("\"") && gpsDataJson.EndsWith("\""))
        {
            Debug.Log("🔄 Detected double-encoded JSON, fixing it...");
            gpsDataJson = gpsDataJson.Trim('"');  // Remove leading/trailing quotes
            gpsDataJson = gpsDataJson.Replace("\\\"", "\"");  // Remove escape characters
        }

        // 🔴 Print cleaned JSON
        Debug.Log($"🟢 CLEANED JSON: {gpsDataJson}");

        GPSData gps;
        try
        {
            gps = JsonConvert.DeserializeObject<GPSData>(gpsDataJson);  // ✅ Use Newtonsoft.Json for flexible parsing
        }
        catch (Exception e)
        {
            Debug.LogError($"❌ JSON Parsing Failed: {e.Message}");
            return Vector2.zero;
        }

        if (gps == null)
        {
            Debug.LogError("❌ GPS data is null after parsing. Check JSON format.");
            return Vector2.zero;
        }

        Vector2 position = new Vector2((float)gps.latitude, (float)gps.longitude);

        // ✅ Print Latitude & Longitude separately in Console
        Debug.Log($"📍 Latitude: {gps.latitude}");
        Debug.Log($"📍 Longitude: {gps.longitude}");
        Debug.Log($"📍 Vector2 Position: {position}");

        return position;
    }

    // 🛰 GPS Data Structure (matches JSON format in Redis)
    [Serializable]
    private class GPSData
    {
        public double latitude;
        public double longitude;
    }
}