// File: GPSRoadDebugger.cs
using UnityEngine;

public class GPSRoadDebugger : MonoBehaviour
{
    public GameObject roadMarkerPrefab; // Assign a small sphere or cube prefab in Unity
    private GPSConverter gpsConverter;

    void Start()
    {
        gpsConverter = FindObjectOfType<GPSConverter>();

        // Add test GPS points along a road in your campus
        AddMarker(13.612263, 100.836828);
        AddMarker(13.612389, 100.836676);
        AddMarker(13.612412, 100.836585);
        AddMarker(13.612441, 100.836478);
        AddMarker(13.612473, 100.836363);
        AddMarker(13.612508, 100.836238);
        AddMarker(13.612534, 100.836147);
        AddMarker(13.612633, 100.835787);
        AddMarker(13.612686, 100.835602);
        AddMarker(13.612740, 100.835415);
        AddMarker(13.612796, 100.835222);
        AddMarker(13.612860, 100.835001);
        AddMarker(13.612950, 100.834689);
        AddMarker(13.613051, 100.834310);
        AddMarker(13.613115, 100.833858);
        AddMarker(13.613170, 100.833660);
        AddMarker(13.613202, 100.833545);
        AddMarker(13.613137, 100.833424);
        AddMarker(13.613034, 100.833390);
        AddMarker(13.612942, 100.833365);
        AddMarker(13.612815, 100.833320);
        AddMarker(13.612710, 100.833239);
        AddMarker(13.612659, 100.833162);
        AddMarker(13.612630, 100.833066);
        AddMarker(13.612630, 100.832969);
        AddMarker(13.612650, 100.832864);
        AddMarker(13.612673, 100.832775);
        AddMarker(13.612708, 100.832643);
        AddMarker(13.612734, 100.832550);
        AddMarker(13.612763, 100.832447);
        AddMarker(13.612791, 100.832348);
        AddMarker(13.612818, 100.832253);
        AddMarker(13.612844, 100.832160);
        AddMarker(13.612869, 100.832071);
        AddMarker(13.612906, 100.831941);
        AddMarker(13.612937, 100.831829);
        AddMarker(13.612962, 100.831739);
        AddMarker(13.612986, 100.831645);
        AddMarker(13.613009, 100.831551);
        AddMarker(13.613034, 100.831459);
        AddMarker(13.613068, 100.831338);
        AddMarker(13.613114, 100.831223);
        AddMarker(13.613205, 100.831203);
        AddMarker(13.613314, 100.831234);
        AddMarker(13.613409, 100.831262);
        AddMarker(13.613503, 100.831290);
        AddMarker(13.613596, 100.831317);
        AddMarker(13.613692, 100.831346);
        AddMarker(13.613789, 100.831373);
        AddMarker(13.613881, 100.831400);
        AddMarker(13.613968, 100.831425);
        AddMarker(13.614096, 100.831462);
        AddMarker(13.614223, 100.831500);
        AddMarker(13.614341, 100.831533);
        AddMarker(13.614444, 100.831560);
        AddMarker(13.614511, 100.831654);
        AddMarker(13.614485, 100.831764);
        AddMarker(13.614458, 100.831869);
        AddMarker(13.614430, 100.831970);
        AddMarker(13.614375, 100.832160);
        AddMarker(13.614337, 100.832286);
        AddMarker(13.614310, 100.832388);
        AddMarker(13.614277, 100.832501);
        AddMarker(13.614220, 100.832695);
        AddMarker(13.614193, 100.832788);
        AddMarker(13.614158, 100.832906);
        AddMarker(13.614128, 100.833007);
        AddMarker(13.614067, 100.833220);
        AddMarker(13.614040, 100.833315);
        AddMarker(13.614005, 100.833436);
        AddMarker(13.613983, 100.833545);
        AddMarker(13.613884, 100.833614);
        AddMarker(13.613775, 100.833604);
        AddMarker(13.613670, 100.833580);
        AddMarker(13.613572, 100.833552);
        AddMarker(13.613482, 100.833526);
        AddMarker(13.613395, 100.833500);
        AddMarker(13.613309, 100.833474);
        AddMarker(13.613202, 100.833545);
        AddMarker(13.613141, 100.833766);
        AddMarker(13.613044, 100.834096);
        AddMarker(13.613051, 100.834310);
        AddMarker(13.613028, 100.834406);
        AddMarker(13.612269, 100.836708);
        AddMarker(13.612305, 100.836591);
        AddMarker(13.612336, 100.836485);
        AddMarker(13.612364, 100.836388);
        AddMarker(13.612392, 100.836290);
        AddMarker(13.612420, 100.836193);
        AddMarker(13.612449, 100.836096);
        AddMarker(13.612505, 100.835902);
        AddMarker(13.612560, 100.835712);
        AddMarker(13.612586, 100.835620);
        AddMarker(13.612612, 100.835529);
        AddMarker(13.612663, 100.835346);
        AddMarker(13.612688, 100.835254);
        AddMarker(13.612738, 100.835072);
        AddMarker(13.612763, 100.834983);
        AddMarker(13.612802, 100.834851);
        AddMarker(13.612840, 100.834719);
        AddMarker(13.612877, 100.834590);
        AddMarker(13.612913, 100.834469);
        AddMarker(13.612950, 100.834343);
        AddMarker(13.612989, 100.834218);
        AddMarker(13.613077, 100.833994);

        AddMarker(13.614978, 100.833956);

        AddMarker(13.615106, 100.833957);

        AddMarker(13.615211, 100.834029);

        AddMarker(13.615104, 100.834055);

        AddMarker(13.614978, 100.833956);
    }

    void AddMarker(double lat, double lon)
    {
        Vector3 unityPos = gpsConverter.ConvertGPSToUnity(lat, lon);
        Instantiate(roadMarkerPrefab, unityPos, Quaternion.identity);
    }
}
