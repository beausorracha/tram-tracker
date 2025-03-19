using UnityEngine;

public class BackgroundMusicController : MonoBehaviour
{
    private static BackgroundMusicController instance;
    private AudioSource musicSource;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Keeps this GameObject alive across scenes

            musicSource = GetComponent<AudioSource>();

            // Load and apply saved volume
            float savedVolume = PlayerPrefs.HasKey("MusicVolume") ? PlayerPrefs.GetFloat("MusicVolume") : 0.5f;
            if (musicSource != null)
            {
                musicSource.volume = savedVolume;
            }
        }
        else
        {
            Destroy(gameObject); // Ensures only one instance exists
        }
    }
}
