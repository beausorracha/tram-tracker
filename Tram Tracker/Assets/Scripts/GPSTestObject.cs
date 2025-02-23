using UnityEngine;

public class GPSTestObject : MonoBehaviour
{
    [Header("GPS Test Settings")]
    public GPSConverter gpsConverter; // Reference to the existing GPS converter
    public Transform testObject; // Assign any object in Unity to test GPS positioning13.612297, 100.836613
    public Vector2 testGPSPosition = new Vector2(13.612297f, 100.836613f); // Set a test GPS coordinate

    void Start()
    {
        if (testObject == null)
        {
            Debug.LogError("Test Object is not assigned!");
            return;
        }

        if (gpsConverter == null)
        {
            Debug.LogError("GPSConverter is not assigned!");
            return;
        }

        // Convert GPS to Unity coordinates
        Vector3 unityPosition = gpsConverter.ConvertGPSToUnity(testGPSPosition.x, testGPSPosition.y);
        testObject.position = unityPosition;

        Debug.Log($"Test Object Placed at Unity Position: {unityPosition} from GPS: {testGPSPosition}");
    }
}