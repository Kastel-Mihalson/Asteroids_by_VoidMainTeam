using UnityEngine;

public sealed class MainMenuStarter : MonoBehaviour
{
    [SerializeField] private MainMenuView _mainMenuView;
    [SerializeField] private SettingsMenuView _settingsMenuView;
    [SerializeField] private GameData _gameData;

    private MainMenuController _mainMenuController;
    private SettingsMenuController _settingsMenuController;
    private AudioController _audioController;

    private void Start()
    {
        _audioController = new AudioController(_gameData.AudioData, _gameData.AudioMixerGroup);
        _mainMenuController = new MainMenuController(_mainMenuView, _gameData);
        _settingsMenuController = new SettingsMenuController(_settingsMenuView, _gameData);
        _mainMenuController.OnEnable();
        _settingsMenuController.OnEnable();

        _audioController.Play(AudioClipManager.NewGameMusic, true);
    }
}
