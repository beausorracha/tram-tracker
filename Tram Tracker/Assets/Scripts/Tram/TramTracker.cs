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
        new Vector2(13.612263f, 100.836828f),
        new Vector2(13.612389f, 100.836676f),
        new Vector2(13.612412f, 100.836585f),
        new Vector2(13.612441f, 100.836478f),
        new Vector2(13.612473f, 100.836363f),
        new Vector2(13.612508f, 100.836238f),
        new Vector2(13.612534f, 100.836147f),
        new Vector2(13.612633f, 100.835787f),
        new Vector2(13.612686f, 100.835602f),
        new Vector2(13.612740f, 100.835415f),
        new Vector2(13.612796f, 100.835222f),
        new Vector2(13.612860f, 100.835001f),
        new Vector2(13.612950f, 100.834689f),
        new Vector2(13.613051f, 100.834310f),
        new Vector2(13.613115f, 100.833858f),
        new Vector2(13.613170f, 100.833660f),
        new Vector2(13.613202f, 100.833545f),
        new Vector2(13.613137f, 100.833424f),
        new Vector2(13.613034f, 100.833390f),
        new Vector2(13.612942f, 100.833365f),
        new Vector2(13.612815f, 100.833320f),
        new Vector2(13.612710f, 100.833239f),
        new Vector2(13.612659f, 100.833162f),
        new Vector2(13.612630f, 100.833066f),
        new Vector2(13.612630f, 100.832969f),
        new Vector2(13.612650f, 100.832864f),
        new Vector2(13.612673f, 100.832775f),
        new Vector2(13.612708f, 100.832643f),
        new Vector2(13.612734f, 100.832550f),
        new Vector2(13.612763f, 100.832447f),
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
        new Vector2(13.613114f, 100.831223f),
        new Vector2(13.613205f, 100.831203f),
        new Vector2(13.613314f, 100.831234f),
        new Vector2(13.613409f, 100.831262f),
        new Vector2(13.613503f, 100.831290f),
        new Vector2(13.613596f, 100.831317f),
        new Vector2(13.613692f, 100.831346f),
        new Vector2(13.613789f, 100.831373f),
        new Vector2(13.613881f, 100.831400f),
        new Vector2(13.613968f, 100.831425f),
        new Vector2(13.614096f, 100.831462f),
        new Vector2(13.614223f, 100.831500f),
        new Vector2(13.614341f, 100.831533f),
        new Vector2(13.614444f, 100.831560f),
        new Vector2(13.614511f, 100.831654f),
        new Vector2(13.614485f, 100.831764f),
        new Vector2(13.614458f, 100.831869f),
        new Vector2(13.614430f, 100.831970f),
        new Vector2(13.614375f, 100.832160f),
        new Vector2(13.614337f, 100.832286f),
        new Vector2(13.614310f, 100.832388f),
        new Vector2(13.614277f, 100.832501f),
        new Vector2(13.614220f, 100.832695f),
        new Vector2(13.614193f, 100.832788f),
        new Vector2(13.614158f, 100.832906f),
        new Vector2(13.614128f, 100.833007f),
        new Vector2(13.614067f, 100.833220f),
        new Vector2(13.614040f, 100.833315f),
        new Vector2(13.614005f, 100.833436f),
        new Vector2(13.613983f, 100.833545f),
        new Vector2(13.613884f, 100.833614f),
        new Vector2(13.613775f, 100.833604f),
        new Vector2(13.613670f, 100.833580f),
        new Vector2(13.613572f, 100.833552f),
        new Vector2(13.613482f, 100.833526f),
        new Vector2(13.613395f, 100.833500f),
        new Vector2(13.613309f, 100.833474f),
        new Vector2(13.613202f, 100.833545f),
        new Vector2(13.613141f, 100.833766f),
        new Vector2(13.613044f, 100.834096f),
        new Vector2(13.613051f, 100.834310f),
        new Vector2(13.613028f, 100.834406f),
        new Vector2(13.612269f, 100.836708f),
        new Vector2(13.612305f, 100.836591f),
        new Vector2(13.612336f, 100.836485f),
        new Vector2(13.612364f, 100.836388f),
        new Vector2(13.612392f, 100.836290f),
        new Vector2(13.612420f, 100.836193f),
        new Vector2(13.612449f, 100.836096f),
        new Vector2(13.612505f, 100.835902f),
        new Vector2(13.612560f, 100.835712f),
        new Vector2(13.612586f, 100.835620f),
        new Vector2(13.612612f, 100.835529f),
        new Vector2(13.612663f, 100.835346f),
        new Vector2(13.612688f, 100.835254f),
        new Vector2(13.612738f, 100.835072f),
        new Vector2(13.612763f, 100.834983f),
        new Vector2(13.612802f, 100.834851f),
        new Vector2(13.612840f, 100.834719f),
        new Vector2(13.612877f, 100.834590f),
        new Vector2(13.612913f, 100.834469f),
        new Vector2(13.612950f, 100.834343f),
        new Vector2(13.612989f, 100.834218f),
        new Vector2(13.613077f, 100.833994f),
        new Vector2(13.613077f, 100.837091f),
        new Vector2(13.613279f, 100.837156f),
        new Vector2(13.613257f, 100.837379f),
        new Vector2(13.613170f, 100.837694f),
        new Vector2(13.613046f, 100.838149f),
        new Vector2(13.612858f, 100.838805f),
        new Vector2(13.612714f, 100.839304f),
        new Vector2(13.612734f, 100.839536f),
        new Vector2(13.612683f, 100.839619f),
        new Vector2(13.612557f, 100.839575f),
        new Vector2(13.612595f, 100.839452f),
        new Vector2(13.612612f, 100.839687f),
        new Vector2(13.612574f, 100.839796f),
        new Vector2(13.612548f, 100.839886f),
        new Vector2(13.612485f, 100.839503f),
        new Vector2(13.612376f, 100.839465f),
        new Vector2(13.612515f, 100.840012f),
        new Vector2(13.612460f, 100.840202f),
        new Vector2(13.612423f, 100.840328f),
        new Vector2(13.612397f, 100.840418f),
        new Vector2(13.612330f, 100.840650f),
        new Vector2(13.612613f, 100.840080f),
        new Vector2(13.612721f, 100.840114f),
        new Vector2(13.612847f, 100.840148f),
        new Vector2(13.612956f, 100.840176f),
        new Vector2(13.612992f, 100.840298f),
        new Vector2(13.612943f, 100.840410f),
        new Vector2(13.612824f, 100.840404f),
        new Vector2(13.612731f, 100.840379f),
        new Vector2(13.612615f, 100.840347f),
        new Vector2(13.612527f, 100.840321f)
        
        

    };

    private int currentTargetIndex = 0;

    void Start()
    {
        if (tram == null)
        {
            Debug.LogError("Tram Transform is not assigned in Inspector!");
            return;
        }

        if (gpsConverter == null)
        {
            Debug.LogError("GPSConverter is not assigned in Inspector!");
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

            // Fix: Rotate the tram correctly
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

    Debug.Log("Tram reached the last test point.");
}


}
