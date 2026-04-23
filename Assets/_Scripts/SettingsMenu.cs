using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public GameObject settingsPanel;

    public Slider volumeSlider;
    public Toggle fullscreenToggle;

    void Start()
    {
        settingsPanel.SetActive(false); // hide at start

        volumeSlider.value = PlayerPrefs.GetFloat("Volume", 1f);
        AudioListener.volume = volumeSlider.value;

        fullscreenToggle.isOn = Screen.fullScreen;
    }

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("Volume", volume);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}