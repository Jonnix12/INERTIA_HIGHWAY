#region

using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

#endregion

public class OptionsCanvas : MonoBehaviour
{
    [Header("Volume Setting")] 
    [SerializeField] private TMP_Text _volumeTextValue;
    [SerializeField] private Slider _volumeSlider;
    [SerializeField] private float _defaultVolume = 1.0f;

    [Header("Resolution Setting")] 
    [SerializeField] private Slider _brightness;
    [SerializeField] private TMP_Text __brightnessTextValue;
    [SerializeField] private float _defaultBrightness = 1;

    private bool _isFullScreen;
    private float _brightnessLevel;

    [Header("Resolution Dropdowns")] public TMP_Dropdown resolutionDropdown;
    private Resolution[] resolutions;

    private void Start()
    {
        _volumeTextValue.text = PlayerPrefs.GetFloat("volumeSettings").ToString("0.0");
        _volumeSlider.value = PlayerPrefs.GetFloat("volumeSettings");

        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetBrighness (float brightness)
    {
        _brightnessLevel = brightness;
        __brightnessTextValue.text = brightness.ToString("0.0");
    }

    public void SetFullScreen(bool isFullScreen)
    {
        _isFullScreen = isFullScreen;
    }

    public void GraphicsApply()
    {
        PlayerPrefs.SetFloat("brightnessSettings", _brightnessLevel);
        PlayerPrefs.SetInt("fullscreenSettings", (_isFullScreen ? 1 : 0));
        Screen.fullScreen = _isFullScreen;
    }


    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        _volumeTextValue.text = volume.ToString("0.0");
    }

    public void VolumeApply()
    {
        PlayerPrefs.SetFloat("volumeSettings", AudioListener.volume);
    }

    public void ResetBuuton(string MenuType)
    {
        if (MenuType == "Audio")
        {
            AudioListener.volume = _defaultVolume;
            _volumeSlider.value = _defaultVolume;
            _volumeTextValue.text = _defaultVolume.ToString("0.0");
            VolumeApply();
        }
    }
}