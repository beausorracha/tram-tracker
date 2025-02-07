using UnityEngine;

public class GPSConverter : MonoBehaviour
{
    // Reference GPS point (Known position on your road)
    public double refLatitude = 13.614202;
    public double refLongitude = 100.832780;

    // Corresponding position of the reference GPS in Unity
    public Vector3 refUnityPosition = new Vector3(250.45f, 0, 600.8f);

    // Manual fine-tuning offset (adjustable in Inspector)
    public Vector3 gpsOffset = new Vector3(-100, 0, -20); // Adjust X & Z as needed
// Start from 0 and adjust later

    // Enable or disable rotation correction
    public bool applyRotation = false;

    public Vector3 ConvertGPSToUnity(double latitude, double longitude)
    {
        double metersPerDegreeLat = 111320;
        double metersPerDegreeLon = 111320 * Mathf.Cos((float)(refLatitude * Mathf.Deg2Rad));

        // Calculate the difference from the reference GPS
        double deltaLat = latitude - refLatitude;
        double deltaLon = longitude - refLongitude;

        // Convert differences to meters
        double xMeters = deltaLon * metersPerDegreeLon;
        double zMeters = deltaLat * metersPerDegreeLat;

        // Convert to Unity world coordinates
        Vector3 unityPos = refUnityPosition + new Vector3((float)zMeters, 0, -(float)xMeters); // Fix axis swap

        // Apply manual fine-tuning
        unityPos += gpsOffset;

        float scaleFactor = 0.95f; // Reduce scale more
        unityPos *= scaleFactor;


        // Optional: Apply rotation correction if needed
        if (applyRotation)
        {
            float angle = 16f * Mathf.Deg2Rad;
            float cos = Mathf.Cos(angle);
            float sin = Mathf.Sin(angle);

            float rotatedX = cos * (unityPos.x - refUnityPosition.x) - sin * (unityPos.z - refUnityPosition.z) + refUnityPosition.x;
            float rotatedZ = sin * (unityPos.x - refUnityPosition.x) + cos * (unityPos.z - refUnityPosition.z) + refUnityPosition.z;

            unityPos = new Vector3(rotatedX, unityPos.y, rotatedZ);
        }

        Debug.Log($"📡 GPS Input: ({latitude}, {longitude}) → Unity Pos: {unityPos}");
        return unityPos;
    }

    // Convert Unity coordinates back to GPS for testing purposes
    public Vector2 ConvertUnityToGPS(Vector3 unityPos)
    {
        double metersPerDegreeLat = 111320;
        double metersPerDegreeLon = 111320 * Mathf.Cos((float)(refLatitude * Mathf.Deg2Rad));

        double deltaX = unityPos.x - refUnityPosition.x;
        double deltaZ = unityPos.z - refUnityPosition.z;

        double lat = refLatitude + (deltaZ / metersPerDegreeLat);
        double lon = refLongitude + (deltaX / metersPerDegreeLon);

        Debug.Log($"🌍 Unity Pos {unityPos} → 🛰️ GPS Pos: ({lat}, {lon})");
        return new Vector2((float)lat, (float)lon);
    }
}
