using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGameMenuView : MonoBehaviour
{
    [SerializeField] private Button _homeButton;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private Text _screenText;
    [SerializeField] private Text _scoreValue;

    private PlayerHUDView _playerHUD;
    private const string WIN_GAME = "<color=#17D133>YOU WIN :)</color>";
    private const string LOSE_GAME = "<color=#D13817>YOU LOSE :(</color>";
    private AudioClipManager _audioClipType;
    private AudioController _audioController;

    private void Start()
    {
        _playerHUD = FindObjectOfType<PlayerHUDView>();

        _homeButton.onClick.AddListener(ToMainMenu);
        _restartButton.onClick.AddListener(RestartGame);
        _exitButton.onClick.AddListener(ExitGame);
        _audioController = new AudioController();
    }

    public void SetScreenActive(bool flag)
    {
        _playerHUD.SetScreenActive(!flag);
        gameObject.SetActive(flag);

        Time.timeScale = flag ? 0 : 1;

        if (flag)
        {
            _audioController.Clear();
            _audioController.Play(_audioClipType, true);
        }
    }

    public void SetGameEndParams(bool isVictory)
    {
        string endGameText;

        if (isVictory)
        {
            endGameText = WIN_GAME;
            _audioClipType = AudioClipManager.VictoryMusic;
        }
        else
        {
            endGameText = LOSE_GAME;
            _audioClipType = AudioClipManager.GameOverMusic;
        }

        var score = _playerHUD.GetScore();

        _scoreValue.text = $"SCORE: {score}";
        _screenText.text = $"{endGameText}";
    }

    private void ToMainMenu()
    {
        _audioController.Clear();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    private void RestartGame()
    {
        SetScreenActive(false);
        _audioController.Clear();
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
}
