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

    [Header("Resolution Dropdowns")] 
    public TMP_Dropdown resolutionDropdown;
    private Resolution[] resolutions;

    // Start is called before the first frame update

    private void Awake()
    {
        RefreshResolutions();
        
    }
    void Start()
    {
        //_isFullScreenToggle.isOn = PlayerPrefs.GetInt("fullscreenSettings") ? 1 : 0;
        _isFullScreenToggle.isOn = Screen.fullScreen;
        if (PlayerPrefs.GetFloat("brightnessSettings") == 0)
            PlayerPrefs.SetFloat("brightnessSettings", 0.5f);
        else
        _brightnessSlider.value = PlayerPrefs.GetFloat("brightnessSettings") * 100;
        __brightnessTextValue.text = _brightnessSlider.value.ToString();
    }

    private void RefreshResolutions()
    {
        resolutions = Screen.resolutions;
        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);
        }

        if (PlayerPrefs.GetInt("resolutionSettings") != 0)
        {
            SetResolution(PlayerPrefs.GetInt("resolutionSettings"));
        }

        else 
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

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
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
        resolutionDropdown.value = index;
        resolutionDropdown.RefreshShownValue();
        PlayerPrefs.SetInt("resolutionSettings", resolutionDropdown.value);
    }

    public void SetBrighness()
    {
        __brightnessTextValue.text = _brightnessSlider.value.ToString();
        Screen.brightness = _brightnessSlider.value / 100;
        PlayerPrefs.SetFloat("brightnessSettings", _brightnessSlider.value / 100);
    }

    public void SetFullScreen()
    {
        Screen.fullScreen = _isFullScreenToggle.isOn;
        PlayerPrefs.SetInt("fullscreenSettings", (_isFullScreenToggle.isOn ? 1 : 0));
    }

    public void ResetButton()
    {
             Screen.brightness = DEFAULT_BRIGHTNESS/100;
            _brightnessSlider.value = DEFAULT_BRIGHTNESS;
            __brightnessTextValue.text = (DEFAULT_BRIGHTNESS).ToString();
            _isFullScreenToggle.isOn = true;
            Screen.fullScreen = true;
            SetResolution(PlayerPrefs.GetInt("resolutionSettings"));
    }
}
