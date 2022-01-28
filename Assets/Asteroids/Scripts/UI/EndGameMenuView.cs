using System;
using UnityEngine;
using UnityEngine.UI;

public sealed class EndGameMenuView : MonoBehaviour
{
    public event Action OnMainMenuButtonClickEvent;
    public event Action OnRestartButtonClickEvent;
    public event Action OnExitButtonClickEvent;

    [SerializeField] private Button _homeButton;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private Text _screenText;
    [SerializeField] private Text _scoreValue;

    public GameObject GameObject => gameObject;

    private void Start()
    {        
        _homeButton.onClick.AddListener(ToMainMenu);
        _restartButton.onClick.AddListener(RestartGame);
        _exitButton.onClick.AddListener(ExitGame);
    }

    private void ExitGame()
    {
        OnExitButtonClickEvent?.Invoke();
    }

    private void RestartGame()
    {
        OnRestartButtonClickEvent?.Invoke();
    }

    private void ToMainMenu()
    {
        OnMainMenuButtonClickEvent?.Invoke();
    }

    public void SetGameResult(int score, string text)
    {
        _scoreValue.text = $"SCORE: {score}";
        _screenText.text = $"{text}";
    }
}
