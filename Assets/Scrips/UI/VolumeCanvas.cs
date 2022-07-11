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
    [SerializeField] private float _defaultVolume = 1.0f;

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