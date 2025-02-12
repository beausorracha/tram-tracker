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
        // AddMarker(13.612263, 100.836828);
        // AddMarker(13.612389, 100.836676);
        // AddMarker(13.612412, 100.836585);
        // AddMarker(13.612441, 100.836478);
        // AddMarker(13.612473, 100.836363);
        // AddMarker(13.612508, 100.836238);
        // AddMarker(13.612534, 100.836147);
        // AddMarker(13.612633, 100.835787);
        // AddMarker(13.612686, 100.835602);
        // AddMarker(13.612740, 100.835415);
        // AddMarker(13.612796, 100.835222);
        // AddMarker(13.612860, 100.835001);
        // AddMarker(13.612950, 100.834689);
        // AddMarker(13.613051, 100.834310);
        // AddMarker(13.613115, 100.833858);
        // AddMarker(13.613170, 100.833660);
        // AddMarker(13.613202, 100.833545);
        // AddMarker(13.613137, 100.833424);
        // AddMarker(13.613034, 100.833390);
        // AddMarker(13.612942, 100.833365);
        // AddMarker(13.612815, 100.833320);
        // AddMarker(13.612710, 100.833239);
        // AddMarker(13.612659, 100.833162);
        // AddMarker(13.612630, 100.833066);
        // AddMarker(13.612630, 100.832969);
        // AddMarker(13.612650, 100.832864);
        // AddMarker(13.612673, 100.832775);
        // AddMarker(13.612708, 100.832643);
        // AddMarker(13.612734, 100.832550);
        // AddMarker(13.612763, 100.832447);
        // AddMarker(13.612791, 100.832348);
        // AddMarker(13.612818, 100.832253);
        // AddMarker(13.612844, 100.832160);
        // AddMarker(13.612869, 100.832071);
        // AddMarker(13.612906, 100.831941);
        // AddMarker(13.612937, 100.831829);
        // AddMarker(13.612962, 100.831739);
        // AddMarker(13.612986, 100.831645);
        // AddMarker(13.613009, 100.831551);
        // AddMarker(13.613034, 100.831459);
        // AddMarker(13.613068, 100.831338);
        // AddMarker(13.613114, 100.831223);
        // AddMarker(13.613205, 100.831203);
        // AddMarker(13.613314, 100.831234);
        // AddMarker(13.613409, 100.831262);
        // AddMarker(13.613503, 100.831290);
        // AddMarker(13.613596, 100.831317);
        // AddMarker(13.613692, 100.831346);
        // AddMarker(13.613789, 100.831373);
        // AddMarker(13.613881, 100.831400);
        // AddMarker(13.613968, 100.831425);
        // AddMarker(13.614096, 100.831462);
        // AddMarker(13.614223, 100.831500);
        // AddMarker(13.614341, 100.831533);
        // AddMarker(13.614444, 100.831560);
        // AddMarker(13.614511, 100.831654);
        // AddMarker(13.614485, 100.831764);
        // AddMarker(13.614458, 100.831869);
        // AddMarker(13.614430, 100.831970);
        // AddMarker(13.614375, 100.832160);
        // AddMarker(13.614337, 100.832286);
        // AddMarker(13.614310, 100.832388);
        // AddMarker(13.614277, 100.832501);
        // AddMarker(13.614220, 100.832695);
        // AddMarker(13.614193, 100.832788);
        // AddMarker(13.614158, 100.832906);
        // AddMarker(13.614128, 100.833007);
        // AddMarker(13.614067, 100.833220);
        // AddMarker(13.614040, 100.833315);
        // AddMarker(13.614005, 100.833436);
        // AddMarker(13.613983, 100.833545);
        // AddMarker(13.613884, 100.833614);
        // AddMarker(13.613775, 100.833604);
        // AddMarker(13.613670, 100.833580);
        // AddMarker(13.613572, 100.833552);
        // AddMarker(13.613482, 100.833526);
        // AddMarker(13.613395, 100.833500);
        // AddMarker(13.613309, 100.833474);
        // AddMarker(13.613202, 100.833545);
        // AddMarker(13.613141, 100.833766);
        // AddMarker(13.613044, 100.834096);
        // AddMarker(13.613051, 100.834310);
        // AddMarker(13.613028, 100.834406);
        // AddMarker(13.612269, 100.836708);
        // AddMarker(13.612305, 100.836591);
        // AddMarker(13.612336, 100.836485);
        // AddMarker(13.612364, 100.836388);
        // AddMarker(13.612392, 100.836290);
        // AddMarker(13.612420, 100.836193);
        // AddMarker(13.612449, 100.836096);
        // AddMarker(13.612505, 100.835902);
        // AddMarker(13.612560, 100.835712);
        // AddMarker(13.612586, 100.835620);
        // AddMarker(13.612612, 100.835529);
        // AddMarker(13.612663, 100.835346);
        // AddMarker(13.612688, 100.835254);
        // AddMarker(13.612738, 100.835072);
        // AddMarker(13.612763, 100.834983);
        // AddMarker(13.612802, 100.834851);
        // AddMarker(13.612840, 100.834719);
        // AddMarker(13.612877, 100.834590);
        // AddMarker(13.612913, 100.834469);
        // AddMarker(13.612950, 100.834343);
        // AddMarker(13.612989, 100.834218);
        // AddMarker(13.613077, 100.833994);
        // AddMarker(13.613077, 100.837091);
        // AddMarker(13.613279, 100.837156);
        // AddMarker(13.613257, 100.837379);
        // AddMarker(13.613170, 100.837694);
        // AddMarker(13.613046, 100.838149);
        // AddMarker(13.612858, 100.838805);
        // AddMarker(13.612714, 100.839304);
        // AddMarker(13.612734, 100.839536);
        // AddMarker(13.612683, 100.839619);
        // AddMarker(13.612557, 100.839575);
        // AddMarker(13.612595, 100.839452);
        // AddMarker(13.612612, 100.839687);
        // AddMarker(13.612574, 100.839796);
        // AddMarker(13.612548, 100.839886);
        // AddMarker(13.612485, 100.839503);
        // AddMarker(13.612376, 100.839465);
        // AddMarker(13.612515, 100.840012);
        // AddMarker(13.612460, 100.840202);
        // AddMarker(13.612423, 100.840328);
        // AddMarker(13.612397, 100.840418);
        // AddMarker(13.612330, 100.840650);
        // AddMarker(13.612613, 100.840080);
        // AddMarker(13.612721, 100.840114);
        // AddMarker(13.612847, 100.840148);
        // AddMarker(13.612956, 100.840176);
        // AddMarker(13.612992, 100.840298);
        // AddMarker(13.612943, 100.840410);
        // AddMarker(13.612824, 100.840404);
        // AddMarker(13.612731, 100.840379);
        // AddMarker(13.612615, 100.840347);
        // AddMarker(13.612527, 100.840321);

        // AddMarker(13.614978, 100.833956);

        // AddMarker(13.615106, 100.833957);

        // AddMarker(13.615211, 100.834029);

        // AddMarker(13.615104, 100.834055);

        // AddMarker(13.614978, 100.833956);

        // AddMarker(13.612068, 100.836771);
        // AddMarker(13.611961, 100.836740);
        // AddMarker(13.611425, 100.836588);
        // AddMarker(13.611327, 100.836555);
        // AddMarker(13.611215, 100.836495);
        // AddMarker(13.611184, 100.836619);
        // AddMarker(13.611160, 100.836713);
        // AddMarker(13.611130, 100.836826);
        // AddMarker(13.611067, 100.837053);
        // AddMarker(13.611001, 100.837284);
        // AddMarker(13.610951, 100.837466);
        // AddMarker(13.610900, 100.837650);
        // AddMarker(13.610850, 100.837833);
        // AddMarker(13.610769, 100.838117);

        // // Sala Thai
        // AddMarker(13.610741, 100.838211);
        // AddMarker(13.610713, 100.838305);
        // AddMarker(13.610647, 100.838485);
        // AddMarker(13.610602, 100.838569);
        // AddMarker(13.610556, 100.838652);
        // AddMarker(13.610510, 100.838734);
        // AddMarker(13.610467, 100.838818);
        // AddMarker(13.610408, 100.839002);
        // AddMarker(13.610383, 100.839095);
        // AddMarker(13.610357, 100.839188);
        // AddMarker(13.610333, 100.839281);
        // AddMarker(13.610317, 100.839375);
        // AddMarker(13.610331, 100.839514);
        // AddMarker(13.610392, 100.839634);
        // AddMarker(13.610491, 100.839725);
        // AddMarker(13.610572, 100.839770);
        // AddMarker(13.610662, 100.839804);
        // AddMarker(13.610758, 100.839828);
        // AddMarker(13.610884, 100.839852);
        // AddMarker(13.611010, 100.839882);
        // AddMarker(13.611105, 100.839911);
        // AddMarker(13.611199, 100.839952);
        // AddMarker(13.611286, 100.840004);
        // AddMarker(13.611364, 100.840068);
        // AddMarker(13.611437, 100.840172);
        // AddMarker(13.611457, 100.840302);
        // AddMarker(13.611466, 100.840398);
        // AddMarker(13.611487, 100.840626);
        // AddMarker(13.611521, 100.840957);
        // AddMarker(13.611556, 100.841334);
        // AddMarker(13.611605, 100.841850);
        // AddMarker(13.611642, 100.842218);
        // AddMarker(13.611682, 100.842624);
        // AddMarker(13.611784, 100.843651);
        // AddMarker(13.611824, 100.844071);
        // AddMarker(13.611865, 100.844482);

        // AddMarker(13.611397, 100.839942);
        // AddMarker(13.611441, 100.839821);
        // AddMarker(13.611501, 100.839706);
        // AddMarker(13.611588, 100.839618);
        // AddMarker(13.611664, 100.839563);
        // AddMarker(13.611773, 100.839504);
        // AddMarker(13.611861, 100.839467);
        // AddMarker(13.611968, 100.839440);
        // AddMarker(13.612082, 100.839427);
        // AddMarker(13.612172, 100.839428);
        // AddMarker(13.612268, 100.839440);
        // AddMarker(13.612376, 100.839465);
        // AddMarker(13.612485, 100.839503);

        // AddMarker(13.61182, 100.83847);
        // AddMarker(13.612858, 100.838805);
        // AddMarker(13.614691, 100.832482);
        // AddMarker(13.615444, 100.832822);
        // AddMarker(13.616157, 100.832935);
        // AddMarker(13.616268, 100.832996);
        // AddMarker(13.616293, 100.833116);
        // AddMarker(13.616310, 100.833348);
        // AddMarker(13.616334, 100.834058);
        // AddMarker(13.616323, 100.834161);
        // AddMarker(13.616281, 100.834245);
        // AddMarker(13.616204, 100.834300);
        // AddMarker(13.616113, 100.834309);
        // AddMarker(13.615994, 100.834276);
        // AddMarker(13.615785, 100.834212);

        // // Sahara
        // AddMarker(13.615054, 100.834178);
        // AddMarker(13.615020, 100.834297);
        // AddMarker(13.614985, 100.834416);
        // AddMarker(13.614947, 100.834543);
        // AddMarker(13.614919, 100.834642);
        // AddMarker(13.614891, 100.834748);
        // AddMarker(13.614879, 100.834858);
        // AddMarker(13.614911, 100.834952);

        // Com Arts
        AddMarker(13.611656, 100.835267);

        // Studio
        AddMarker(13.611883, 100.834515);

        // VMES
        AddMarker(13.613432, 100.836200);        

    }

    void AddMarker(double lat, double lon)
    {
        Vector3 unityPos = gpsConverter.ConvertGPSToUnity(lat, lon);
        Instantiate(roadMarkerPrefab, unityPos, Quaternion.identity);
    }
}
