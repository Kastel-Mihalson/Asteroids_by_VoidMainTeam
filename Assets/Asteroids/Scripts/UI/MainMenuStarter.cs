using UnityEngine;

public sealed class MainMenuStarter : MonoBehaviour
{
    [SerializeField] private AudioData _audioData;

    private MainMenuController _mainMenuController;
    private AudioController _audioController;

    private void Start()
    {
        _audioController = new AudioController(_audioData);
        _mainMenuController = new MainMenuController(_audioController);
        _mainMenuController.OnEnable();
    }
}
