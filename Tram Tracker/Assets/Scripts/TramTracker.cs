using System;
using UnityEngine;
using StackExchange.Redis;

public class TramTracker : MonoBehaviour
{
    private ConnectionMultiplexer redis;
    private IDatabase db;

    public string tramID = "tram_1"; // Change for different trams
    public float latitude;
    public float longitude;

    // Tram movement speed
    public float speed = 5.0f;

    void Start()
    {
        try
        {
            // Connect to Redis
            redis = ConnectionMultiplexer.Connect("redis-13242.crce178.ap-east-1-1.ec2.redns.redis-cloud.com:13242,password=z1WTBRd81HGrGckawMz6oHtHNOoXAAR3");
            db = redis.GetDatabase();
            Debug.Log("✅ Connected to Redis!");
        }
        catch (Exception e)
        {
            Debug.LogError("❌ Redis Connection Error: " + e.Message);
        }

        InvokeRepeating("UpdateTramPosition", 1.0f, 1.0f); // Fetch data every second
    }

    void UpdateTramPosition()
    {
        try
        {
            string latString = db.StringGet($"gps:{tramID}:latitude");
            string lonString = db.StringGet($"gps:{tramID}:longitude");

            if (!string.IsNullOrEmpty(latString) && !string.IsNullOrEmpty(lonString))
            {
                // Explicitly cast to float to prevent errors
                latitude = (float)Convert.ToDouble(latString);
                longitude = (float)Convert.ToDouble(lonString);

                Debug.Log($"📡 Received GPS Data: Latitude {latitude}, Longitude {longitude}");
                MoveTram(latitude, longitude);
            }
            else
            {
                Debug.LogWarning("⚠️ No GPS data found in Redis.");
            }
        }
        catch (Exception e)
        {
            Debug.LogError("❌ Error fetching GPS data: " + e.Message);
        }
    }

    void MoveTram(float lat, float lon)
    {
        Vector3 worldPos = GPS2Unity(lat, lon);
        transform.position = Vector3.Lerp(transform.position, worldPos, Time.deltaTime * speed);
    }

    Vector3 GPS2Unity(float lat, float lon)
    {
        // Convert GPS coordinates to Unity world coordinates
        float x = (lon - 100.832f) * 10000f; // Adjust scaling factor
        float z = (lat - 13.612f) * 10000f;  // Adjust scaling factor
        return new Vector3(x, 0, z); // Y is 0 for ground-level movement
    }
}
