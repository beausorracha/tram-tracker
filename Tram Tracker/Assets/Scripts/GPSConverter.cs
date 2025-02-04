
using UnityEngine;

public class GPSConverter : MonoBehaviour
{
    // Reference GPS point (A road in your school)
    public double refLatitude = 13.612273188085357;  
    public double refLongitude = 100.83661200989619;

    // The corresponding position of the new road in Unity
    public Vector3 refUnityPosition = new Vector3(205.45f, 0, 533.8f);  // Updated starting position

    public Vector3 ConvertGPSToUnity(double latitude, double longitude)
    {
        // Convert GPS degrees to meters
        double metersPerDegreeLat = 111320;
        double metersPerDegreeLon = 111320 * Mathf.Cos((float)(refLatitude * Mathf.Deg2Rad));
        
        // Calculate difference from the reference road
        double deltaLat = latitude - refLatitude;
        double deltaLon = longitude - refLongitude;

        // Convert to meters
        double xMeters = deltaLon * metersPerDegreeLon;
        double zMeters = deltaLat * metersPerDegreeLat;

        // Map to Unity world
        // return refUnityPosition + new Vector3((float)xMeters, 0, (float)zMeters);
        // return refUnityPosition + new Vector3((float)zMeters, 0,(float)xMeters);
        // return refUnityPosition + new Vector3(-(float)xMeters, 0, (float)zMeters);
        Vector3 unityPos = refUnityPosition + new Vector3((float)zMeters, 0, -(float)xMeters);

        // Apply 15-degree rotation to the left
        float angle = 16f * Mathf.Deg2Rad; // Convert to radians
        float cos = Mathf.Cos(angle);
        float sin = Mathf.Sin(angle);

        float rotatedX = cos * (unityPos.x - refUnityPosition.x) - sin * (unityPos.z - refUnityPosition.z) + refUnityPosition.x;
        float rotatedZ = sin * (unityPos.x - refUnityPosition.x) + cos * (unityPos.z - refUnityPosition.z) + refUnityPosition.z;

        return new Vector3(rotatedX, unityPos.y, rotatedZ);


    }
}