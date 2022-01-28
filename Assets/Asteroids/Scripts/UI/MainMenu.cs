using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public sealed class MainMenu : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _quitButton;
    [SerializeField] private GameObject _settingsPanel;

    private AudioController _audioController;

    private void Start()
    {
        _startButton.onClick.AddListener(StartGame);
        _settingsButton.onClick.AddListener(OpenSettings);
        _quitButton.onClick.AddListener(QuitGame);

        _audioController = new AudioController();
        _audioController.Play(AudioClipManager.NewGameMusic, true);
    }

    private void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void OpenSettings()
    {
        if (!_settingsPanel.activeSelf)
        {
            _settingsPanel.SetActive(true);
            gameObject.SetActive(false);
        }
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
