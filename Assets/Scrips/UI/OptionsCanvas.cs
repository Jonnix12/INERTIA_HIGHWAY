#region

using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

#endregion

public class OptionsCanvas : MonoBehaviour
{
    [Header("Volume Setting")] [SerializeField]
    private TMP_Text volumeTextValue;

    [SerializeField] private Slider volumeSlider;
    [SerializeField] private float defaultVolume = 1.0f;


    [Header("Resolution Dropdowns")] public TMP_Dropdown resolutionDropdown;
    private Resolution[] resolutions;

    private void Start()
    {
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

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        volumeSlider.value = defaultVolume;
        volumeTextValue.text = volume.ToString("0.0");
        VolumeApply();
    }

    public void VolumeApply()
    {
        PlayerPrefs.SetFloat("volumeSettings", AudioListener.volume);
    }

    public void ResetBuuton(string MenuType)
    {
        if (MenuType == "Audio")
        {
            AudioListener.volume = defaultVolume;
            volumeSlider.value = defaultVolume;
            volumeTextValue.text = defaultVolume.ToString("0.0");
        }
    }
}