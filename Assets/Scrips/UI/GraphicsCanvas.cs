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
    [SerializeField] private const int DEFAULT_BRIGHTNESS = 50;

    private float _brightnessLevel;

    [Header("Resolution Dropdowns")] 
    public TMP_Dropdown resolutionDropdown;
    private Resolution[] resolutions;

    // Start is called before the first frame update
    void Start()
    {
        resolutions = Screen.resolutions;
        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);
        }

        if (PlayerPrefs.GetInt("resolutionSettings") == 0)
        {
            for (int i = 0; i < resolutions.Length; i++)
            {
                if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
                {
                    currentResolutionIndex = i;
                    SetResolution(i);
                    PlayerPrefs.SetInt("resolutionSettings", i);
                    break;
                }
            }
        }

        else
            SetResolution(PlayerPrefs.GetInt("resolutionSettings"));

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        
        _isFullScreenToggle.isOn = Screen.fullScreen;
        _brightnessSlider.value = PlayerPrefs.GetFloat("brightnessSettings") * 100;
    }

    public void SetResolution()
    {
        Resolution resolution = resolutions[resolutionDropdown.value];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        PlayerPrefs.SetInt("resolutionSettings", resolutionDropdown.value);
    }

    private void SetResolution(int index)
    {
        Resolution resolution = resolutions[index];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        PlayerPrefs.SetInt("resolutionSettings", resolutionDropdown.value);
    }

    public void SetBrighness(float brightness)
    {
        _brightnessLevel = brightness;
        __brightnessTextValue.text = brightness.ToString();
    }

    public void SetFullScreen()
    {
            Screen.fullScreen = _isFullScreenToggle.isOn;
    }

    public void GraphicsApply()
    {
        Screen.brightness = _brightnessLevel;
        PlayerPrefs.SetFloat("brightnessSettings", _brightnessLevel/100);
        PlayerPrefs.SetInt("fullscreenSettings", (_isFullScreenToggle.isOn ? 1 : 0));
        SetResolution();
        SetFullScreen();
    }

    public void ResetButton()
    {
            _brightnessSlider.value = DEFAULT_BRIGHTNESS;
            __brightnessTextValue.text = DEFAULT_BRIGHTNESS.ToString();
            _isFullScreenToggle.isOn = true;
            Screen.fullScreen = true;
            SetResolution(PlayerPrefs.GetInt("resolutionSettings"));
            GraphicsApply();
    }
}
