using UnityEngine;
using System.Collections.Generic;
using OptionData = TMPro.TMP_Dropdown.OptionData;

public sealed class SettingsMenuController
{
    private const string MAIN_VOLUME = "MainVolume";
    private Resolution[] _resolutions;
    private SettingsMenuView _view;
    private GameData _gameData;

    public SettingsMenuController(SettingsMenuView view, GameData gameData)
    {
        _view = view;
        _gameData = gameData;
        InitResolutionsToggle();
        InitVolumeSlider();
    }

    private void InitVolumeSlider()
    {
        if (_gameData.AudioMixerGroup.audioMixer.GetFloat(MAIN_VOLUME, out float volume))
        {
            _view.SetVolumeSliderValue(volume);
        }
    }

    public void OnEnable()
    {
        _view.OnVolumeSliderValueChangedEvent += SetVolume;
        _view.OnGraphicsDropdownValueChangedEvent += SetQuality;
        _view.OnFullscreenToggleValueChangedEvent += SetFullscreen;
        _view.OnResolutionDropdownValueChangedEvent += SetResolution;
    }

    public void OnDisable()
    {
        _view.OnVolumeSliderValueChangedEvent -= SetVolume;
        _view.OnGraphicsDropdownValueChangedEvent -= SetQuality;
        _view.OnFullscreenToggleValueChangedEvent -= SetFullscreen;
        _view.OnResolutionDropdownValueChangedEvent -= SetResolution;
    }

    private void SetVolume(float volume)
    {
        _gameData.AudioMixerGroup.audioMixer.SetFloat(MAIN_VOLUME, volume);
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

        _view.UpdateToggle(resolutions, defaultResolutionIndex);
    }
}
