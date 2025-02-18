using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    private AudioSource musicSource;
    public Slider volumeSlider;

    void Start()
    {
        // Find the MusicManager in the scene
        GameObject musicManager = GameObject.Find("MusicManager");
        if (musicManager != null)
        {
            musicSource = musicManager.GetComponent<AudioSource>();
        }

        // Load the saved volume setting
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            volumeSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        }
        else
        {
            volumeSlider.value = 0.5f; // Default value
        }

        // Apply initial volume
        if (musicSource != null)
        {
            musicSource.volume = volumeSlider.value;
        }

        // Add listener to update volume dynamically
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    public void SetVolume(float volume)
    {
        if (musicSource != null)
        {
            musicSource.volume = volume;
            PlayerPrefs.SetFloat("MusicVolume", volume); // Save the setting
        }
    }
}
