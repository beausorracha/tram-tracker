using UnityEngine;

public class BackgroundMusicController : MonoBehaviour
{
    private static BackgroundMusicController instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Keeps this GameObject alive across scenes
        }
        else
        {
            Destroy(gameObject); // Ensures only one instance exists
        }
    }
}
