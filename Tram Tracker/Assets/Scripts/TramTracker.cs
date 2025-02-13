using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TramTracker : MonoBehaviour
{
    [Header("Tram Movement Settings")]
    public GPSConverter gpsConverter; // Reference to GPSConverter
    public Transform tram; // Assign your tram object in Inspector
    public float moveSpeed = 5f; // Adjust for smoother movement

    [Header("Fixed GPS Test Data")]
    private List<Vector2> gpsTestPoints = new List<Vector2>
    {
        
        new Vector2(13.612791f, 100.832348f),
        new Vector2(13.612818f, 100.832253f),
        new Vector2(13.612844f, 100.832160f),
        new Vector2(13.612869f, 100.832071f),
        new Vector2(13.612906f, 100.831941f),
        new Vector2(13.612937f, 100.831829f),
        new Vector2(13.612962f, 100.831739f),
        new Vector2(13.612986f, 100.831645f),
        new Vector2(13.613009f, 100.831551f),
        new Vector2(13.613034f, 100.831459f),
        new Vector2(13.613068f, 100.831338f),
        new Vector2(13.613114f, 100.831223f)
    };

    private int currentTargetIndex = 0;

    void Start()
    {
        if (tram == null)
        {
            Debug.LogError("🚨 Tram Transform is not assigned in Inspector!");
            return;
        }

        if (gpsConverter == null)
        {
            Debug.LogError("🚨 GPSConverter is not assigned in Inspector!");
            return;
        }

        // Set tram at the first GPS point
        Vector3 startPos = gpsConverter.ConvertGPSToUnity(gpsTestPoints[0].x, gpsTestPoints[0].y);
        tram.position = startPos;

        StartCoroutine(MoveTramAlongTestPath());
    }

    IEnumerator MoveTramAlongTestPath()
{
    while (currentTargetIndex < gpsTestPoints.Count - 1)
    {
        Vector2 gpsPoint = gpsTestPoints[currentTargetIndex];
        Vector2 nextGpsPoint = gpsTestPoints[currentTargetIndex + 1];

        Vector3 startPos = gpsConverter.ConvertGPSToUnity(gpsPoint.x, gpsPoint.y);
        Vector3 targetPos = gpsConverter.ConvertGPSToUnity(nextGpsPoint.x, nextGpsPoint.y);

        float totalDistance = Vector3.Distance(startPos, targetPos);
        float journeyTime = totalDistance / moveSpeed; // Adjust based on distance
        float journey = 0f;

        while (journey < 1f) // Ensures smooth and gradual movement
        {
            journey += Time.deltaTime / journeyTime;
            tram.position = Vector3.Lerp(startPos, targetPos, journey);

            // 🔥 Fix: Rotate the tram correctly
            Vector3 direction = (targetPos - tram.position).normalized;
            if (direction != Vector3.zero)
            {
                tram.rotation = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 90, 0); // Adjust rotation
            }

            yield return null; // Wait for next frame (NO delays)
        }

        // Move to the next GPS point
        currentTargetIndex++;
    }

    Debug.Log("✅ Tram reached the last test point.");
}


}
