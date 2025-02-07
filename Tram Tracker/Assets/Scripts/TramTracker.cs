using UnityEngine;
using UnityEngine.Splines;

public class TramTracker : MonoBehaviour
{
    public SplineContainer spline;
    public float speed = 5.0f;

    private float currentT = 0f;

    public float testLatitude = 13.614202f;
    public float testLongitude = 100.832780f;

    private GPSConverter gpsConverter;

    void Start()
    {
        gpsConverter = FindObjectOfType<GPSConverter>();

        if (gpsConverter == null)
        {
            Debug.LogError("❌ GPSConverter not found!");
            return;
        }

        Vector3 gpsWorldPosition = GPS2Unity(testLatitude, testLongitude);
        AlignTramToSpline(gpsWorldPosition);

        PrintSplinePoints();
    }

    void Update()
    {
        MoveAlongSpline();
    }

    void AlignTramToSpline(Vector3 worldPos)
    {
        float closestT = FindClosestPointOnSpline(worldPos);
        currentT = closestT;

        Vector3 closestPosition = spline.Spline.EvaluatePosition(closestT);
        transform.position = closestPosition;

        Vector3 direction = spline.Spline.EvaluateTangent(closestT);
        transform.rotation = Quaternion.LookRotation(direction);

        DebugPosition(worldPos, Color.red, "GPS Position");
        DebugPosition(closestPosition, Color.blue, "Closest Spline Position");
    }

    void MoveAlongSpline()
    {
        if (currentT < 1.0f)
        {
            currentT += speed * Time.deltaTime / spline.Spline.GetLength();
            transform.position = spline.Spline.EvaluatePosition(currentT);
            transform.rotation = Quaternion.LookRotation(spline.Spline.EvaluateTangent(currentT));
        }
    }

    float FindClosestPointOnSpline(Vector3 worldPos)
    {
        float closestT = 0f;
        float minDistance = float.MaxValue;

        for (float t = 0; t <= 1.0f; t += 0.01f)
        {
            Vector3 splinePos = spline.Spline.EvaluatePosition(t);
            float distance = Vector3.Distance(worldPos, splinePos);

            if (distance < minDistance)
            {
                minDistance = distance;
                closestT = t;
            }
        }

        return closestT;
    }

    Vector3 GPS2Unity(float lat, float lon)
    {
        return gpsConverter.ConvertGPSToUnity(lat, lon);
    }

    void DebugPosition(Vector3 position, Color color, string label)
    {
        GameObject marker = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        marker.transform.position = position;
        marker.transform.localScale = Vector3.one * 2;
        marker.GetComponent<Renderer>().material.color = color;
    }

    void PrintSplinePoints()
    {
        for (float t = 0; t <= 1.0f; t += 0.05f)
        {
            Debug.Log($"🛤️ Spline Unity Position at t={t}: {spline.Spline.EvaluatePosition(t)}");
        }
    }
}
