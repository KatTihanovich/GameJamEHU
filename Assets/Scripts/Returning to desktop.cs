using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExitButton : MonoBehaviour
{
    public string targetScene = "Desktop"; // Название сцены, на которую переходим
    public string runnerScene = "Runner";
    public string scaryMazeScene = "ScaryMaze";
    public string antiVirusWindowScene = "AntyvirusWindow";
    public string taskManagerScene = "TaskManager";
    public Button exitButton;

    void Start()
    {
        if (exitButton != null)
        {
            exitButton.onClick.AddListener(GoToDesktop);
        }
    }

    public void GoToDesktop()
    {
        SceneManager.LoadScene(targetScene);
    }

    public void GoToRunner()
    {
        SceneManager.LoadScene(runnerScene);
    }

    public void GoToScaryMaze()
    {
        SceneManager.LoadScene(scaryMazeScene);
    }
    public void GoToAntyvirusWindow()
    {
        SceneManager.LoadScene(antiVirusWindowScene);
    }

    public void GoToTaskManager()
    {
        SceneManager.LoadScene(taskManagerScene);
    }
    public static void RestartGame()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
