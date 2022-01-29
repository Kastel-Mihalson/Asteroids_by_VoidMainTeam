using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;
using System.Collections.Generic;
using OptionData = TMPro.TMP_Dropdown.OptionData;

public sealed class SettingsMenuView : MonoBehaviour
{
    [SerializeField] private Button _backButton;
    [SerializeField] private GameObject _mainMenuPanel;
    [SerializeField] private Slider _volumeSlider;
    [SerializeField] private TMP_Dropdown _graphicsDropdown;
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private Toggle _fullscreenToggle;
    [SerializeField] private TMP_Dropdown _resolutionDropdown;

    private void Start()
    {
        _backButton.onClick.AddListener(OpenMainMenu);
        _volumeSlider.onValueChanged.AddListener(SetVolume);
        _graphicsDropdown.onValueChanged.AddListener(SetQuality);
        _fullscreenToggle.onValueChanged.AddListener(SetFullscreen);
        _resolutionDropdown.onValueChanged.AddListener(SetResolution);
    }

    private void OpenMainMenu()
    {
        if (!_mainMenuPanel.activeSelf)
        {
            _mainMenuPanel.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    private void SetVolume(float volume)
    {
        
    }

    private void SetQuality(int qualityLevelIndex)
    {
       
    }

    private void SetFullscreen(bool isFullscreen)
    {

    }

    private void SetResolution(int resolutionIndex)
    {
       
    }

    public void UpdateToggle(List<OptionData> resolutions, int defaultResolutionIndex)
    {
        _resolutionDropdown.ClearOptions();
        _resolutionDropdown.AddOptions(resolutions);
        _resolutionDropdown.value = defaultResolutionIndex;
        _resolutionDropdown.RefreshShownValue();
    }
}
