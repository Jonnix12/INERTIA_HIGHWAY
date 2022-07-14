#region

using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

#endregion

public class VolumeCanvas : MonoBehaviour
{
    [Header("Volume Setting")] 
    [SerializeField] private TMP_Text _volumeTextValue;
    [SerializeField] private Slider _volumeSlider;
    [SerializeField] private int _defaultVolume = 50;

    private void Start()
    {
        AudioListener.volume = PlayerPrefs.GetFloat("volumeSettings") * 100;
        _volumeSlider.value = PlayerPrefs.GetFloat("volumeSettings")*100;
    }

    public void SetVolume()
    {
        AudioListener.volume = _volumeSlider.value;
        _volumeTextValue.text = _volumeSlider.value.ToString();
        PlayerPrefs.SetFloat("volumeSettings", AudioListener.volume / 100f);
    }

    public void ResetButton()
    {
            AudioListener.volume = _defaultVolume;
            _volumeSlider.value = _defaultVolume;
            _volumeTextValue.text = _volumeSlider.value.ToString();
    }
}