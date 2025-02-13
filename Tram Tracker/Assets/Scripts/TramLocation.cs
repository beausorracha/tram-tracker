using UnityEngine;

public class TramLocation : MonoBehaviour
{
    public static TramLocation Instance;
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

    public void SetNextStation(string stationName)
    {
        nextStation = stationName;
    }
}
