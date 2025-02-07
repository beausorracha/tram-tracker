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
        new Vector2((float)13.612263, (float)100.836828),
        new Vector2((float)13.612389, (float)100.836676),
        new Vector2((float)13.612412, (float)100.836585),
        new Vector2((float)13.612441, (float)100.836478),
        new Vector2((float)13.612473, (float)100.836363),
        new Vector2((float)13.612508, (float)100.836238),
        new Vector2((float)13.612534, (float)100.836147),
        new Vector2((float)13.612633, (float)100.835787),
        new Vector2((float)13.612686, (float)100.835602),
        new Vector2((float)13.612740, (float)100.835415),
        new Vector2((float)13.612796, (float)100.835222),
        new Vector2((float)13.612860, (float)100.835001),
        new Vector2((float)13.612950, (float)100.834689),
        new Vector2((float)13.613051, (float)100.834310),
        new Vector2((float)13.613115, (float)100.833858),
        new Vector2((float)13.613170, (float)100.833660),
        new Vector2((float)13.613202, (float)100.833545),
        new Vector2((float)13.613137, (float)100.833424),
        new Vector2((float)13.613034, (float)100.833390),
        new Vector2((float)13.612942, (float)100.833365),
        new Vector2((float)13.612815, (float)100.833320),
        new Vector2((float)13.612710, (float)100.833239),
        new Vector2((float)13.612659, (float)100.833162),
        new Vector2((float)13.612630, (float)100.833066),
        new Vector2((float)13.612630, (float)100.832969),
        new Vector2((float)13.612650, (float)100.832864),
        new Vector2((float)13.612673, (float)100.832775),
        new Vector2((float)13.612708, (float)100.832643),
        new Vector2((float)13.612734, (float)100.832550),
        new Vector2((float)13.612763, (float)100.832447),
        new Vector2((float)13.612791, (float)100.832348),
        new Vector2((float)13.612818, (float)100.832253),
        new Vector2((float)13.612844, (float)100.832160),
        new Vector2((float)13.612869, (float)100.832071),
        new Vector2((float)13.612906, (float)100.831941),
        new Vector2((float)13.612937, (float)100.831829),
        new Vector2((float)13.612962, (float)100.831739),
        new Vector2((float)13.612986, (float)100.831645),
        new Vector2((float)13.613009, (float)100.831551),
        new Vector2((float)13.613034, (float)100.831459),
        new Vector2((float)13.613068, (float)100.831338),
        new Vector2((float)13.613114, (float)100.831223)
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

        StartCoroutine(MoveTramAlongTestPath());
    }

    IEnumerator MoveTramAlongTestPath()
    {
        while (true)
        {
            if (currentTargetIndex >= gpsTestPoints.Count)
            {
                Debug.Log("✅ Tram reached the last test point.");
                yield break; // Stop moving when all points are reached
            }

            // Get next GPS position
            Vector2 gpsPoint = gpsTestPoints[currentTargetIndex];
            Vector3 targetPos = gpsConverter.ConvertGPSToUnity(gpsPoint.x, gpsPoint.y);

            // Move towards the target position smoothly
            while (Vector3.Distance(tram.position, targetPos) > 0.1f)
            {
                tram.position = Vector3.Lerp(tram.position, targetPos, Time.deltaTime * moveSpeed);
                
                // Align tram with movement direction
                Vector3 direction = (targetPos - tram.position).normalized;
                if (direction != Vector3.zero)
                {
                    tram.rotation = Quaternion.LookRotation(direction);
                }

                yield return null;
            }

            // Move to the next GPS point
            currentTargetIndex++;
            yield return new WaitForSeconds(1.5f); // Small delay between movements
        }
    }
}
