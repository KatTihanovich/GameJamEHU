using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class EmailManager : MonoBehaviour
{
    public Button[] emailButtons;
    public Button[] deleteButtons;
    public Button playVideoButton;
    public Button jumpscareButton;
    public Button imageButton;

    public GameObject jumpscareImage;
    public GameObject otherImage;
    public GameObject restartPanel;

    private float startTime;
    private bool targetEmailDeleted = false;

    void Start()
    {
        startTime = Time.time;

        foreach (Button btn in deleteButtons)
        {
            btn.onClick.AddListener(() => DeleteEmail(btn));
        }

        playVideoButton.onClick.AddListener(GoToVideoScene);
        jumpscareButton.onClick.AddListener(ShowJumpscare);
        imageButton.onClick.AddListener(ShowOtherImage);

        ResetUI();
    }

    void ResetUI()
    {
        foreach (Button btn in emailButtons)
        {
            btn.interactable = true;
            btn.gameObject.SetActive(true);
        }

        foreach (Button btn in deleteButtons)
        {
            btn.interactable = true;
            btn.gameObject.SetActive(true);
        }

        jumpscareImage.SetActive(false);
        otherImage.SetActive(false);
        restartPanel.SetActive(false);
    }

    void DeleteEmail(Button deleteButton)
    {
        int index = System.Array.IndexOf(deleteButtons, deleteButton);
        if (index >= 0 && index < emailButtons.Length)
        {
            emailButtons[index].interactable = false;
            emailButtons[index].gameObject.SetActive(false);
            deleteButton.interactable = false;
            deleteButton.gameObject.SetActive(false);

            if (!targetEmailDeleted && index == 2)
            {
                targetEmailDeleted = true;
                float timeTaken = Time.time - startTime;
                HeatBar.DecreaseHeat(1f);
                Debug.Log("Время удаления нужного письма: " + timeTaken + " секунд");
            }
            else
            {
                HeatBar.IncreaseHeat(1f);
            }
        }
    }

    void GoToVideoScene()
    {
        PlayerPrefs.SetString("ReturnScene", SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("VideoVirusScene");
    }

    void ShowJumpscare()
    {
        jumpscareImage.SetActive(true);
        StartCoroutine(HideJumpscareAfterDelay(5f));
    }

    IEnumerator HideJumpscareAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        jumpscareImage.SetActive(false);
    }

    void ShowOtherImage()
    {
        StartCoroutine(RestartLevelAfterDelay(2f));
    }

    IEnumerator RestartLevelAfterDelay(float delay)
    {
        otherImage.SetActive(true);
        yield return new WaitForSeconds(delay);
        otherImage.SetActive(false);
        restartPanel.SetActive(true);
        yield return new WaitForSeconds(10f);
        ResetUI();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
