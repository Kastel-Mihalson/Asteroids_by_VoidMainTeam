using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Audio;
using OptionData = TMPro.TMP_Dropdown.OptionData;

public sealed class SettingsMenuController
{
    private const string MAIN_VOLUME = "MainVolume";
    private Resolution[] _resolutions;
    private SettingsMenuView _view;
    private AudioMixer _audioMixer;

    public SettingsMenuController(SettingsMenuView view)
    {
        _view = view;
        _audioMixer = _view.AudioMixer;
        InitResolutionsToggle();
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

        _view.UpdateToggle(resolutions, defaultResolutionIndex);
    }
}
