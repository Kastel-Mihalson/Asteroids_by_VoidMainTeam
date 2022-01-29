using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class MainMenuController
{
    private MainMenuView _view;
    private AudioController _audioController;

    public MainMenuController(AudioController audioController)
    {
        _audioController = audioController;
        _audioController.Play(AudioClipManager.NewGameMusic, true);
        _view = Object.FindObjectOfType<MainMenuView>();
    }

    public void OnEnable()
    {
        _view.OnStartGameButtonClickEvent += StartGame;
        _view.OnExitButtonClickEvent += QuitGame;
    }

    public void OnDisable()
    {
        _view.OnStartGameButtonClickEvent -= StartGame;
        _view.OnExitButtonClickEvent -= QuitGame;
    }

    private void StartGame()
    {
        _audioController.Clear();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
