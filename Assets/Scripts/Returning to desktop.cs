using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExitButton : MonoBehaviour
{
    public string targetScene = "Desktop"; // Название сцены, на которую переходим
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
}
