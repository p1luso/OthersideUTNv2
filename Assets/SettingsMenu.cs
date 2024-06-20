using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer; // Renombrado para mayor claridad
    public Slider volumeSlider;
    public TMP_Dropdown graphicsDropdown;
    public int dropdownValue;

    private void Start()
    {
        dropdownValue = graphicsDropdown.value;
        volumeSlider.onValueChanged.AddListener(SetVolume); // Añadir listener para el slider
    }

    public void SetVolume(float volume)
    {
        // Supongamos que el parámetro expuesto en tu AudioMixer se llama "Volume"
        audioMixer.SetFloat("Volume", Mathf.Log10(volume) * 20); 
    }

    public void SetQuality(int dropdownValue)
    {
        QualitySettings.SetQualityLevel(dropdownValue);
    }
}