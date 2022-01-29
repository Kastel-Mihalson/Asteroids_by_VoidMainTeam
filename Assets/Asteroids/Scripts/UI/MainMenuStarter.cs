using UnityEngine;

public sealed class MainMenuStarter : MonoBehaviour
{
    [SerializeField] private AudioData _audioData;
    [SerializeField] private MainMenuView _mainMenuView;
    [SerializeField] private SettingsMenuView _settingsMenuView;

    private MainMenuController _mainMenuController;
    private SettingsMenuController _settingsMenuController;
    private AudioController _audioController;

    private void Start()
    {
        _audioController = new AudioController(_audioData);
        _mainMenuController = new MainMenuController(_mainMenuView, _audioController);
        _settingsMenuController = new SettingsMenuController(_settingsMenuView);
        _mainMenuController.OnEnable();
        _settingsMenuController.OnEnable();
    }
}
