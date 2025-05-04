using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider volumeSlider;

    private void Start()
    {
        // Load saved volume or use default
        float savedVolume = PlayerPrefs.GetFloat("MasterVolume", 1f);
        volumeSlider.value = savedVolume;
        SetVolume(savedVolume);
    }

    public void SetVolume(float volume)
    {
        // Convert linear 0-1 to logarithmic -80 to 0 dB
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);

        // Save the volume setting
        PlayerPrefs.SetFloat("MasterVolume", volume);
        PlayerPrefs.Save();
    }

    public void ToggleMute(bool isMuted)
    {
        if (isMuted)
        {
            audioMixer.SetFloat("MasterVolume", -80); // Mute
        }
        else
        {
            SetVolume(volumeSlider.value); // Restore to slider value
        }
    }
}
