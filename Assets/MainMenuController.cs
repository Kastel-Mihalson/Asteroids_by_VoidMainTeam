using UnityEngine;
using UnityEngine.UI;

public sealed class MainMenuController : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _quitButton;

    private void Start()
    {
        _startButton.onClick.AddListener(StartGame);
        _settingsButton.onClick.AddListener(OpenSettings);
        _quitButton.onClick.AddListener(QuitGame);
    }

    private void StartGame()
    {

    }

    private void OpenSettings()
    {

    }

    private void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}
