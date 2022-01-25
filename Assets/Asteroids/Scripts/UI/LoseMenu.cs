using UnityEngine;
using UnityEngine.UI;

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
        //gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameObject.SetActive(true);
            Time.timeScale = 0;
            //UnityEditor.EditorApplication.isPaused = true;
        }
    }

    private void ToMainMenu()
    {
        Debug.Log("Load main menu screen");
        //SceneManager.LoadScene(SceneManager.GetSceneByName("MainMenu"));
    }

    private void RestartGame()
    {
        Debug.Log("Restart game");
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void ExitGame()
    {
        Debug.Log("Exit game");
        //gameObject.SetActive(false);
        //UnityEditor.EditorApplication.isPlaying = false;
    }
 }
