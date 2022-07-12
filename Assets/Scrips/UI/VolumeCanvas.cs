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
        _volumeSlider.value = PlayerPrefs.GetFloat("volumeSettings")*100;
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        _volumeTextValue.text = volume.ToString();
    }

    public void VolumeApply()
    {
        PlayerPrefs.SetFloat("volumeSettings", AudioListener.volume/100f);
    }

    public void ResetBuuton(string MenuType)
    {
        if (MenuType == "Audio")
        {
            AudioListener.volume = _defaultVolume;
            _volumeSlider.value = _defaultVolume;
            _volumeTextValue.text = _defaultVolume.ToString();
            VolumeApply();
        }
    }
}