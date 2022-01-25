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
    private int _enemyLayer;
    private const string WIN_GAME = "<color=#17D133>YOU WIN :)</color>";
    private const string LOSE_GAME = "<color=#D13817>YOU LOSE :(</color>";

    private void Start()
    {
        _enemyLayer = LayerMask.NameToLayer("Enemy");
        _playerHUD = FindObjectOfType<PlayerHUDView>();

        _homeButton.onClick.AddListener(ToMainMenu);
        _restartButton.onClick.AddListener(RestartGame);
        _exitButton.onClick.AddListener(ExitGame);
    }

    public void SetScreenActive(bool flag)
    {
        _playerHUD.SetScreenActive(!flag);
        gameObject.SetActive(flag);

        Time.timeScale = flag ? 0 : 1;
    }

    public void SetGameEndParams(int gameObjectLayer)
    {
        var endGameText = gameObjectLayer == _enemyLayer ? WIN_GAME : LOSE_GAME;
        var score = _playerHUD.GetScore();

        _scoreValue.text = $"SCORE: {score}";
        _screenText.text = $"{endGameText}";
    }

    private void ToMainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    private void RestartGame()
    {
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
}
