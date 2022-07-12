using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GraphicsCanvas : MonoBehaviour
{
    [Header("Graphics Setting")]
    [SerializeField] private Slider _brightnessSlider;
    [SerializeField] private Toggle _isFullScreenToggle;
    [SerializeField] private TMP_Text __brightnessTextValue;
    [SerializeField] private int _defaultBrightness = 50;

    private bool _isFullScreen;
    private float _brightnessLevel;

    [Header("Resolution Dropdowns")] public TMP_Dropdown resolutionDropdown;
    private Resolution[] resolutions;
    // Start is called before the first frame update
    void Start()
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

        _isFullScreenToggle.isOn = Screen.fullScreen;
        _brightnessSlider.value = PlayerPrefs.GetFloat("brightnessSettings") * 100;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetBrighness(float brightness)
    {
        _brightnessLevel = brightness;
        __brightnessTextValue.text = brightness.ToString();
    }

    public void SetFullScreen(bool isFullScreen)
    {
        _isFullScreen = isFullScreen;
    }

    public void GraphicsApply()
    {
        Screen.brightness = _brightnessLevel;
        PlayerPrefs.SetFloat("brightnessSettings", _brightnessLevel/100);
        PlayerPrefs.SetInt("fullscreenSettings", (_isFullScreen ? 1 : 0));
        Screen.fullScreen = _isFullScreen;
    }

    public void ResetBuuton(string MenuType)
    {
        if (MenuType == "Graphics")
        {
            _brightnessSlider.value = _defaultBrightness;
            __brightnessTextValue.text = _defaultBrightness.ToString();
            _isFullScreenToggle.isOn = true;
            Screen.fullScreen = true;

            Resolution currentResolution = Screen.currentResolution;
            Screen.SetResolution(currentResolution.width, currentResolution.height, Screen.fullScreen);
            resolutionDropdown.value = resolutions.Length;
            GraphicsApply();
        }
    }
}
