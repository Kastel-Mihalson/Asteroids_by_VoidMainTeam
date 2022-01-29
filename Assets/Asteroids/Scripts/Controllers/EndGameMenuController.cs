using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class EndGameMenuController
{
    private PlayerHUDView _playerHUD;
    private EndGameMenuModel _model;
    private EndGameMenuView _view;
    private AudioClipManager _audioClipType;
    private AudioController _audioController;

    public EndGameMenuController(AudioController audioController)
    {
        _model = new EndGameMenuModel();
        _audioController = audioController;
        _view = Object.FindObjectOfType<EndGameMenuView>();
        _playerHUD = Object.FindObjectOfType<PlayerHUDView>();

        SetScreenActive(false);
    }

    public void SetScreenActive(bool flag)
    {
        _playerHUD.SetScreenActive(!flag);
        _view.GameObject.SetActive(flag);

        Time.timeScale = flag ? 0 : 1;
    }

    public void SetGameEndParams(bool isVictory)
    {
        string endGameText;

        if (isVictory)
        {
            endGameText = EndGameMenuModel.WIN_GAME;
            _audioClipType = AudioClipManager.VictoryMusic;
        }
        else
        {
            endGameText = EndGameMenuModel.LOSE_GAME;
            _audioClipType = AudioClipManager.GameOverMusic;
        }

        _model.Score = _playerHUD.GetScore();

        _view.SetGameResult(_model.Score, endGameText);
    }

    private void ToMainMenu()
    {
        _audioController.Clear();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    private void RestartGame()
    {
        _audioController.Clear();
        SetScreenActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void OnEnable()
    {
        _view.OnMainMenuButtonClickEvent += ToMainMenu;
        _view.OnExitButtonClickEvent += ExitGame;
        _view.OnRestartButtonClickEvent += RestartGame;
        _view.OnSetScreenParamsEvent += ShowEndGameMenu;
    }

    public void OnDisable()
    {
        _view.OnMainMenuButtonClickEvent -= ToMainMenu;
        _view.OnExitButtonClickEvent -= ExitGame;
        _view.OnRestartButtonClickEvent -= RestartGame;
        _view.OnSetScreenParamsEvent -= ShowEndGameMenu;
    }

    private void ShowEndGameMenu(bool isVictory)
    {
        SetGameEndParams(isVictory);

        _audioController.Clear();
        _audioController.Play(_audioClipType, true);

        SetScreenActive(true); 
    }
}
