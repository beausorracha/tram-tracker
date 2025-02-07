using UnityEngine;
using UnityEngine.Splines; // Make sure Unity's Spline package is installed
using Unity.Mathematics;  // Required for float3

public class GPSConverter : MonoBehaviour
{
    // Reference GPS point (A road in your school)
    public double refLatitude = 13.612273188085357;  
    public double refLongitude = 100.83661200989619;

    // The corresponding position of the new road in Unity
    public Vector3 refUnityPosition = new Vector3(205.45f, 0, 533.8f);  // Updated starting position

    [Header("Spline Settings")]
    public SplineContainer roadSpline;  // Assign your road spline in Inspector

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
        Vector3 unityPos = refUnityPosition + new Vector3((float)zMeters, 0, -(float)xMeters);

        // Apply 15-degree rotation to the left
        float angle = 16f * Mathf.Deg2Rad; // Convert to radians
        float cos = Mathf.Cos(angle);
        float sin = Mathf.Sin(angle);

        float rotatedX = cos * (unityPos.x - refUnityPosition.x) - sin * (unityPos.z - refUnityPosition.z) + refUnityPosition.x;
        float rotatedZ = sin * (unityPos.x - refUnityPosition.x) + cos * (unityPos.z - refUnityPosition.z) + refUnityPosition.z;

        Vector3 finalPos = new Vector3(rotatedX, unityPos.y, rotatedZ);

        // Debug: Show sphere positions BEFORE snapping
        Debug.Log($"🟢 Before Spline Snapping: {finalPos}");

        // Snap to the closest spline point
        Vector3 snappedPos = GetClosestPointOnSpline(finalPos);

        // Debug: Show sphere positions AFTER snapping
        Debug.Log($"🔵 After Spline Snapping: {snappedPos}");

        return snappedPos;
    }

    private Vector3 GetClosestPointOnSpline(Vector3 unityPos)
    {
        if (roadSpline == null || roadSpline.Spline == null)
        {
            Debug.LogWarning("⚠️ No spline found! Returning raw position.");
            return unityPos;
        }

        float closestDistance = float.MaxValue;
        Vector3 closestPoint = unityPos;

        // Iterate through spline knots (road markers) and find the closest one
        foreach (var knot in roadSpline.Spline.Knots)
        {
            Vector3 point = knot.Position;
            float distance = Vector3.Distance(unityPos, point);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestPoint = point;
            }
        }

        Debug.Log($"📍 Snapping to Spline: Closest Point {closestPoint}, Distance: {closestDistance}");

        // If the closest distance is too far, we ignore snapping (keep the original position)
        if (closestDistance > 5f)  // Adjust this threshold if needed
        {
            Debug.Log("🚨 Closest point too far, keeping original position.");
            return unityPos;
        }

        return closestPoint;
    }
}
