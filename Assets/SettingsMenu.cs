using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;
public class SettingsMenu : MonoBehaviour
{
    public AudioSource source;
    public Slider volumeSlider;
    public TMP_Dropdown graphicsDropdown;
    public int dropdownValue;

    private void Start()
    {
        dropdownValue = graphicsDropdown.value;
    }
public void SetVolume ()
    {
        source.volume = volumeSlider.value;
    }

    public void SetQuality (int dropdownValue)
    {
        QualitySettings.SetQualityLevel(dropdownValue);
    
    }

        



}
