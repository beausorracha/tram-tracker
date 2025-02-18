using UnityEngine;
using Unity.Cinemachine;

public class CameraSwitching : MonoBehaviour
{
    public CinemachineCamera TramCam;
    public CinemachineCamera MapCam;

    public void SwitchCamera()
    {
        // Swap priorities
        int tempPriority = TramCam.Priority;
        TramCam.Priority = MapCam.Priority;
        MapCam.Priority = tempPriority;
    }
}
