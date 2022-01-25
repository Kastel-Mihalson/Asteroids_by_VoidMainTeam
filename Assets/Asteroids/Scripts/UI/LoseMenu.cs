using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoseMenu : MonoBehaviour
{
    [SerializeField] private Button _homeButton;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _exitButton;

    private void Start()
    {
        _homeButton.onClick.AddListener(ToMainMenu);
        _restartButton.onClick.AddListener(RestartGame);
        _exitButton.onClick.AddListener(ExitGame);
        gameObject.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameObject.SetActive(true);
            UnityEditor.EditorApplication.isPaused = true;
        }
    }

    private void ToMainMenu()
    {
        //SceneManager.LoadScene(SceneManager.GetSceneByName("MainMenu"));
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void ExitGame()
    {
        gameObject.SetActive(false);
        UnityEditor.EditorApplication.isPlaying = false;
    }
 }
