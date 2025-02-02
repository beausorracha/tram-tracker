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
        AddMarker(13.612286, 100.836785);
        AddMarker(13.612273, 100.836612);
        AddMarker(13.612376, 100.836248);
        AddMarker(13.612602, 100.835434);
        AddMarker(13.613021, 100.833988);
        AddMarker(13.613144, 100.833435);
        AddMarker(13.613049, 100.833378);
        AddMarker(13.612848, 100.833310);
        AddMarker(13.612745, 100.833266);
        AddMarker(13.612666, 100.833202);
        AddMarker(13.612621, 100.833131);
        AddMarker(13.612596, 100.833032);
        AddMarker(13.612635, 100.832805);
        AddMarker(13.612740, 100.832432);
        AddMarker(13.612944, 100.831726);
        AddMarker(13.613112, 100.831217);
        AddMarker(13.613139, 100.831188);
        AddMarker(13.613189, 100.831187);
        AddMarker(13.613257, 100.831210);
        AddMarker(13.613460, 100.831272);
        AddMarker(13.613857, 100.831386);
        AddMarker(13.614293, 100.831511);
        AddMarker(13.614442, 100.831569);
        AddMarker(13.614474, 100.831613);
        AddMarker(13.614461, 100.831703);
        AddMarker(13.614401, 100.831909);
        AddMarker(13.614325, 100.832194);
        AddMarker(13.614224, 100.832540);
        AddMarker(13.614093, 100.833020);
        AddMarker(13.614003, 100.833371);
        AddMarker(13.613872, 100.833539);
        AddMarker(13.613704, 100.833560);
        AddMarker(13.613496, 100.833507);
        AddMarker(13.613313, 100.833463);
        AddMarker(13.613215, 100.833568);
        AddMarker(13.613164, 100.833745);
        AddMarker(13.613090, 100.834023);
        AddMarker(13.612924, 100.834675);
        AddMarker(13.612835, 100.834994);
        AddMarker(13.612738, 100.835316);
        AddMarker(13.612621, 100.835729);
        AddMarker(13.612509, 100.836155);
        AddMarker(13.612442, 100.836380);
        AddMarker(13.612394, 100.836558);
    }

    void AddMarker(double lat, double lon)
    {
        Vector3 unityPos = gpsConverter.ConvertGPSToUnity(lat, lon);
        Instantiate(roadMarkerPrefab, unityPos, Quaternion.identity);
    }
}
