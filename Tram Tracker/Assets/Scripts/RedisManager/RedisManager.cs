using System;
using UnityEngine;
using StackExchange.Redis;
using System.Threading.Tasks;
using Newtonsoft.Json;  // JSON Parsing
using System.Collections; // Fix for IEnumerator
using UnityEngine.UI;  // UI Warning Text Support
using TMPro; 

public class RedisManager : MonoBehaviour
{
    private static ConnectionMultiplexer redis;
    private static IDatabase db;
    private static bool isConnected = false; // Track connection status

    private string redisHost = "redis-13242.crce178.ap-east-1-1.ec2.redns.redis-cloud.com";
    private int redisPort = 13242;
    private string redisPassword = "z1WTBRd81HGrGckawMz6oHtHNOoXAAR3"; 

    public TextMeshProUGUI warningText; // Use TextMeshPro for better rendering


    async void Start()
    {
        await ConnectToRedis();
        if (isConnected)
        {
            //Debug.Log("Redis Connected Successfully!");
            StartCoroutine(UpdateGPSDataLoop());  // Start auto-fetching GPS data
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
            ShowWarning("Redis Connection Failed! Check Internet.");
            //Debug.LogError($"Redis Connection Failed: {ex.Message}");
        }
    }

    private IEnumerator UpdateGPSDataLoop()
    {
        while (true)
        {
            yield return FetchGPSData(); // Properly waits without blocking Unity
            //  // Wait for result (avoids implicit conversion issue)
            yield return new WaitForSeconds(1.0f);
        }
    }

    public async Task<Vector2> FetchGPSData()
    {
        if (!isConnected || db == null)
        {
            ShowWarning("⚠️ Redis not connected. Reconnecting...");
            await ConnectToRedis(); // Try reconnecting
            if (!isConnected) return Vector2.zero;
        }

        //Debug.Log("Fetching GPS data from Redis...");

        string gpsDataJson = await db.StringGetAsync("gps:tram_1");

        if (string.IsNullOrEmpty(gpsDataJson))
        {
            ShowWarning("No GPS data found!");
            return Vector2.zero;
        }

        // 🔹 Clean up double-encoded JSON if necessary
        if (gpsDataJson.StartsWith("\"") && gpsDataJson.EndsWith("\""))
        {
            //Debug.Log("Detected double-encoded JSON, fixing it...");
            gpsDataJson = gpsDataJson.Trim('"').Replace("\\\"", "\"");
        }

        //Debug.Log($"CLEANED JSON: {gpsDataJson}");

        GPSData gps;
        try
        {
            gps = JsonConvert.DeserializeObject<GPSData>(gpsDataJson);
        }
        catch (Exception e)
        {
            ShowWarning("GPS data error.");
            //Debug.LogError($"JSON Parsing Failed: {e.Message}");
            return Vector2.zero;
        }

        if (gps == null || gps.latitude == null || gps.longitude == null)
        {
            ShowWarning("Tram is not receiving fixed GPS data.");
            return Vector2.zero;
        }

        HideWarning(); // GPS is valid, hide warning
        //Debug.Log($"GPS Position: {gps.latitude}, {gps.longitude}");

        return new Vector2((float)gps.latitude, (float)gps.longitude);
    }

    void ShowWarning(string message)
    {
        if (warningText != null)
        {
            warningText.text = message;
            warningText.enabled = true;
        }
    }

    void HideWarning()
    {
        if (warningText != null)
        {
            warningText.enabled = false;
        }
    }

    [Serializable]
    private class GPSData
    {
        public double? latitude; // Allow nullable values
        public double? longitude;
    }
}