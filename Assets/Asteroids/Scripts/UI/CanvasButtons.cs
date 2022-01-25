using UnityEngine.SceneManagement;
using UnityEngine;

public class CanvasButtons : MonoBehaviour
{
    public void ToHomeMenu()
    {

    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitGame()
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
