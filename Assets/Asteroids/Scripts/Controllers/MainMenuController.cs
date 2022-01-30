using UnityEngine.SceneManagement;

public sealed class MainMenuController
{
    private MainMenuView _view;
    private GameData _gameData;

    public MainMenuController(MainMenuView view, GameData gameData)
    {
        _view = view;
        _gameData = gameData;
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
