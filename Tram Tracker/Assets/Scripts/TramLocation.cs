using UnityEngine;

public class TramLocation : MonoBehaviour
{
    public static TramLocation Instance;
    public Vector3 tramPosition;
    public string nextStation;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdateTramPosition(Vector3 newPosition)
    {
        tramPosition = newPosition;
    }

    public void SetNextStation(string stationName)
    {
        nextStation = stationName;
    }
}
