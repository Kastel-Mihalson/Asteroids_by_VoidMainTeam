using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;
using System.Collections.Generic;
using OptionData = TMPro.TMP_Dropdown.OptionData;

public sealed class SettingsMenu : MonoBehaviour
{
    [SerializeField] private Button _backButton;
    [SerializeField] private GameObject _mainMenuPanel;
    [SerializeField] private Slider _volumeSlider;
    [SerializeField] private TMP_Dropdown _graphicsDropdown;
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private Toggle _fullscreenToggle;
    [SerializeField] private TMP_Dropdown _resolutionDropdown;

    private const string MAIN_VOLUME = "MainVolume";
    private Resolution[] _resolutions;

    private void Start()
    {
        _backButton.onClick.AddListener(OpenMainMenu);
        _volumeSlider.onValueChanged.AddListener(SetVolume);
        _graphicsDropdown.onValueChanged.AddListener(SetQuality);
        _fullscreenToggle.onValueChanged.AddListener(SetFullscreen);
        _resolutionDropdown.onValueChanged.AddListener(SetResolution);

        InitResolutionsToggle();        
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
        _audioMixer.SetFloat(MAIN_VOLUME, volume);
    }

    private void SetQuality(int qualityLevelIndex)
    {
        QualitySettings.SetQualityLevel(qualityLevelIndex);
    }

    private void SetFullscreen(bool isFullscreen)
    {
#if UNITY_EDITOR
        var editorWindow = UnityEditor.EditorWindow.focusedWindow;
        editorWindow.maximized = isFullscreen;
#else
        Screen.fullScreen = isFullscreen;
#endif
    }

    private void SetResolution(int resolutionIndex)
    {
        Resolution resolution = _resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    private void InitResolutionsToggle()
    {
        _resolutions = Screen.resolutions;

        int defaultResolutionIndex = 0;
        List<OptionData> resolutions = new List<OptionData>();
        for (int i = 0; i < Screen.resolutions.Length; i++)
        {
            Resolution resolution = _resolutions[i];
            string resolutionText = $"{resolution.width} x {resolution.height}";
            OptionData optionData = new OptionData(resolutionText);
            resolutions.Add(optionData);

            if (resolution.width == Screen.currentResolution.width
                && resolution.height == Screen.currentResolution.height)
            {
                defaultResolutionIndex = i;
            }
        }

        _resolutionDropdown.ClearOptions();
        _resolutionDropdown.AddOptions(resolutions);
        _resolutionDropdown.value = defaultResolutionIndex;
        _resolutionDropdown.RefreshShownValue();
    }
}
